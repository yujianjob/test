
package com.landaojia.washclothes.userappserver.service.sms.yimei.ws;

import javax.xml.ws.Endpoint;

/**
 * This class was generated by Apache CXF 3.0.4
 * 2015-03-16T15:52:16.611+08:00
 * Generated source version: 3.0.4
 * 
 */
 
public class SDKClient_SDKService_Server{

    protected SDKClient_SDKService_Server() throws java.lang.Exception {
        System.out.println("Starting Server");
        Object implementor = new SDKServiceImpl();
        String address = "http://sdk999ws.eucp.b2m.cn:8080/sdk/SDKService";
        Endpoint.publish(address, implementor);
    }
    
    public static void main(String args[]) throws java.lang.Exception { 
        new SDKClient_SDKService_Server();
        System.out.println("Server ready..."); 
        
        Thread.sleep(5 * 60 * 1000); 
        System.out.println("Server exiting");
        System.exit(0);
    }
}
