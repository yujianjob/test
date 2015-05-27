package com.landaojia.washclothes.laundry.client;

import javafx.animation.Interpolator;
import javafx.animation.KeyFrame;
import javafx.animation.KeyValue;
import javafx.animation.TimelineBuilder;
import javafx.application.Application;
import javafx.application.ConditionalFeature;
import javafx.application.Platform;
import javafx.beans.value.ChangeListener;
import javafx.beans.value.ObservableValue;
import javafx.concurrent.Worker.State;
import javafx.event.ActionEvent;
import javafx.event.EventHandler;
import javafx.scene.DepthTest;
import javafx.scene.Node;
import javafx.scene.PerspectiveCamera;
import javafx.scene.Scene;
import javafx.scene.control.ToolBar;
import javafx.scene.image.Image;
import javafx.scene.input.MouseEvent;
import javafx.scene.layout.BorderPane;
import javafx.scene.layout.GridPane;
import javafx.scene.layout.HBox;
import javafx.scene.layout.Priority;
import javafx.scene.layout.Region;
import javafx.scene.layout.StackPane;
import javafx.stage.Stage;
import javafx.stage.StageStyle;
import javafx.util.Duration;
import netscape.javascript.JSObject;

import com.landaojia.washclothes.laundry.client.client.ClientJsApi;
import com.landaojia.washclothes.laundry.client.client.Html5WebView;
import com.landaojia.washclothes.laundry.client.client.WindowButtons;
import com.landaojia.washclothes.laundry.client.client.WindowResizeButton;
import com.landaojia.washclothes.laundry.client.util.PathUtil;

/**
 * 洗衣工厂客户端程序
 * @author liuxi
 */
public class ClientApplication extends Application{
    /**
     * UI界面初始化控件
     */
    private BorderPane root;
    private Scene scene;
    private WindowResizeButton windowResizeButton;
    private WindowButtons windowButtons;
    private ToolBar toolBar;
    private double mouseDragOffsetX = 0;
    private double mouseDragOffsetY = 0;
    private StackPane modalDimmer;
    
    private Html5WebView browser;
    
    private volatile static ClientApplication factoryClient;
    
    private final String mainPageUrl = "http://192.168.1.193:9628/laundry-web/";
    //private final String mainPageUrl = "http://115.29.232.208:9628/laundry-web/";

    private ClientJsApi clientJsApi;

	@Override
	public void start(Stage stage) throws Exception {
        initUI(stage);
        stage.getIcons().add( new Image( getClass().getResourceAsStream( "/META-INF/image/icon_40_40.png" ) ) );
        stage.show();
        windowButtons.toogleMaximized();
	}

    private void initUI(final Stage stage) {
    	factoryClient = this;

        stage.setTitle( "懒到家-工厂系统" );
        stage.initStyle(StageStyle.UNDECORATED);

        windowResizeButton = new WindowResizeButton(stage, 1020, 700);
        
        root = new BorderPane() {
            @Override
            protected void layoutChildren() {
                super.layoutChildren();
                windowResizeButton.autosize();
                windowResizeButton.setLayoutX(getWidth() - windowResizeButton.getLayoutBounds().getWidth());
                windowResizeButton.setLayoutY(getHeight() - windowResizeButton.getLayoutBounds().getHeight());
            }
        };
        root.getStyleClass().add("application");
        root.setId( "root" );

        StackPane layerPane = new StackPane();
        layerPane.setDepthTest(DepthTest.DISABLE);
        layerPane.getChildren().add(root);
        boolean is3dSupported = Platform.isSupported(ConditionalFeature.SCENE3D);
        scene = new Scene(layerPane, 1020, 700, is3dSupported);
        if (is3dSupported) {
            scene.setCamera(new PerspectiveCamera());
        }
        scene.getStylesheets().add(this.getClass().getResource("/META-INF/css/laundry-client.css").toExternalForm());

        modalDimmer = new StackPane();
        modalDimmer.setId("ModalDimmer");
        modalDimmer.setOnMouseClicked(new EventHandler<MouseEvent>() {
            public void handle(MouseEvent t) {
                t.consume();
                hideModalMessage();
            }
        });
        modalDimmer.setVisible(false);
        layerPane.getChildren().add(modalDimmer);

        toolBar = new ToolBar();
        toolBar.setId("mainToolBar");
        
        //ImageView logo = new ImageView(new Image(this.getClass().getResourceAsStream("/META-INF/images/logo.png")));
        //HBox.setMargin(logo, new Insets(0, 0, 0, 5));
        //toolBar.getItems().add(logo);
        
        Region spacer = new Region();
        HBox.setHgrow(spacer, Priority.ALWAYS);
        toolBar.getItems().add(spacer);
        toolBar.setPrefHeight(20);
        toolBar.setMinHeight(20);
        toolBar.setMaxHeight(20);
        GridPane.setConstraints(toolBar, 0, 0);
        windowButtons = new WindowButtons(stage, false);
        toolBar.getItems().add(windowButtons);
        toolBar.setOnMouseClicked(new EventHandler<MouseEvent>() {
            @Override
            public void handle(MouseEvent event) {
                if (event.getClickCount() == 2) {
                    windowButtons.toogleMaximized();
                }
            }
        });
        toolBar.setOnMousePressed(new EventHandler<MouseEvent>() {
            @Override
            public void handle(MouseEvent event) {
                mouseDragOffsetX = event.getSceneX();
                mouseDragOffsetY = event.getSceneY();
            }
        });
        toolBar.setOnMouseDragged(new EventHandler<MouseEvent>() {
            @Override
            public void handle(MouseEvent event) {
                if (!windowButtons.isMaximized()) {
                    stage.setX(event.getScreenX() - mouseDragOffsetX);
                    stage.setY(event.getScreenY() - mouseDragOffsetY);
                }
            }
        });
        this.root.setTop(toolBar);
        
        browser = new Html5WebView();
        browser.getWebEngine().getLoadWorker().stateProperty().addListener(new ChangeListener<State>() {
            @Override
            public void changed(ObservableValue<? extends State> ov, State oldState, State newState) {
                if (newState == State.SUCCEEDED) {
                    JSObject win = (JSObject) browser.executeScript("window");
                    clientJsApi = new ClientJsApi();
                    win.setMember( "clientJsApi", clientJsApi );
                    
                    //----主页自动执行脚本(只能在FX主线程里执行)----------------
                    //boolean isMainIndex = mainPageUrl.equals(browser.getWebEngine().getLocation());
                    //if (isMainIndex) {
                    //    JSObject winFn = (JSObject) browser.executeScript("mpos.sdk.event.onBootstrap");
                    //    if (winFn != null)
                    //        browser.executeScript("mpos.sdk.event.onBootstrap()");
                    //}
            		
                    //Platform.runLater(new Runnable() {
	        		//	public void run() {
	        		//		
	        		//	}
                    //});
                }
            }
        });

        browser.load( mainPageUrl );
        this.root.setCenter(browser);

        windowResizeButton.setManaged(false);
        this.root.getChildren().add(windowResizeButton);

        stage.setScene(scene);
        stage.sizeToScene();
    }

    public void executeScript(final String script) {
        Platform.runLater(new Runnable() {
            @Override
            public void run() {
                browser.executeScript(script);
            }
        });
    }
    
    @SuppressWarnings("unused")
    private void showModalMessage(Node message) {
        modalDimmer.getChildren().add(message);
        modalDimmer.setOpacity(0);
        modalDimmer.setVisible(true);
        modalDimmer.setCache(true);
        TimelineBuilder.create().keyFrames(new KeyFrame(Duration.seconds(1), new EventHandler<ActionEvent>() {
            public void handle(ActionEvent t) {
                modalDimmer.setCache(false);
            }
        }, new KeyValue(modalDimmer.opacityProperty(), 1, Interpolator.EASE_BOTH))).build().play();
    }
    
    private void hideModalMessage() {
        modalDimmer.setCache(true);
        TimelineBuilder.create().keyFrames(new KeyFrame(Duration.seconds(1), new EventHandler<ActionEvent>() {
            public void handle(ActionEvent t) {
                modalDimmer.setCache(false);
                modalDimmer.setVisible(false);
                modalDimmer.getChildren().clear();
            }
        }, new KeyValue(modalDimmer.opacityProperty(), 0, Interpolator.EASE_BOTH))).build().play();
    }
    
    public static void main(String[] args) {
    	try{
            if (factoryClient == null) {
                synchronized (ClientApplication.class) {
                    if (factoryClient == null) {
                        launch(ClientApplication.class);
                    }
                }
            }
    	}catch( Exception e ){
    		e.printStackTrace();
    	}

    }
}
