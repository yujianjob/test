package com.landaojia.washclothes.laundry.client.client;

import javafx.event.EventHandler;
import javafx.geometry.HPos;
import javafx.geometry.VPos;
import javafx.scene.layout.Region;
import javafx.scene.web.PopupFeatures;
import javafx.scene.web.PromptData;
import javafx.scene.web.WebEngine;
import javafx.scene.web.WebEvent;
import javafx.scene.web.WebView;
import javafx.util.Callback;

/**
 * Html5浏览器（ webkit）
 * @author liuxi
 */
public class Html5WebView extends Region {
    /**
     * 默认设置浏览器的宽度和高度
     */
    private static final int DEFAULT_PREF_WIDTH = 800;
    private static final int DEFAULT_PREF_HEIGHT = 600;
    private static final String DEFAULT_PAGE_URL = "about:";

    /**
     * 宽度属性
     */
    private double prefWidth = DEFAULT_PREF_WIDTH;

    /**
     * 高度属性
     */
    private double prefHeight = DEFAULT_PREF_HEIGHT;

    /**
     * webkit view UI
     */
    private WebView webView;

    /**
     * webkit engine引擎
     */
    private WebEngine webEngine;

    /**
     * 默认构造函数
     */
    public Html5WebView() {
        this(DEFAULT_PREF_WIDTH, DEFAULT_PREF_HEIGHT);
    }

    /**
     * 指定宽度和高度构造函数
     * @param prefWidth
     * @param prefHeight
     */
    public Html5WebView(double prefWidth, double prefHeight) {
        super();
        this.prefWidth = prefWidth;
        this.prefHeight = prefHeight;
        initBrowser();
    }

    /**
     * 初始化浏览器的处理
     */
    protected void initBrowser() {
        webView = new WebView();
        webView.setContextMenuEnabled(false);

        webEngine = this.webView.getEngine();
        webEngine.setJavaScriptEnabled(true);

        webView.setCache(true);

        // JS alter('')方法Handler
        webEngine.setOnAlert(new EventHandler<WebEvent<String>>() {
            public void handle(WebEvent<String> arg0) {
                System.out.println("触发setOnAlert事件：" + arg0);
            };
        });

        // JS open('')方法handler
        webEngine.setCreatePopupHandler(new Callback<PopupFeatures, WebEngine>() {
            public WebEngine call(PopupFeatures arg0) {
                System.out.println("触发setCreatePopupHandler事件：" + arg0);
                return null;
            };
        });

        // JS prompt
        webEngine.setPromptHandler(new Callback<PromptData, String>() {
            public String call(PromptData arg0) {
                System.out.println("触发setPromptHandler事件：" + arg0.getMessage() + "-" + arg0.getDefaultValue());
                return null;
            };
        });

        // JS Confirm
        webEngine.setConfirmHandler(new Callback<String, Boolean>() {
            @Override
            public Boolean call(String arg0) {
                System.out.println("触发setConfirmHandler事件：" + arg0);
                return true;
            }
        });

        webEngine.load(DEFAULT_PAGE_URL);

        getChildren().add(webView);
    }

    @Override
    protected double computePrefWidth(double arg0) {
        return prefWidth;
    }

    @Override
    protected double computeMinHeight(double arg0) {
        return prefHeight;
    }

    @Override
    public void setPrefSize(double arg0, double arg1) {
        super.setPrefSize(arg0, arg1);
        this.webView.setPrefSize(arg0, arg1);
    }

    @Override
    protected void layoutChildren() {
        double w = getWidth();
        double h = getHeight();
        layoutInArea(webView, 0, 0, w, h, 0, HPos.CENTER, VPos.CENTER);
    }

    public WebEngine getWebEngine() {
        return webEngine;
    }

    /**
     * 加载Navigator页面
     * @param url
     */
    public void load(String url) {
        getWebEngine().load(url);
    }

    public void loadContent(String content) {
        getWebEngine().loadContent(content);
    }

    public Object executeScript(String script) {
        try {
            Object result = getWebEngine().executeScript(script);
            return result;
        } catch (Exception e) {
        	e.printStackTrace();
            return null;
        }
    }

}
