package com.landaojia.washclothes.laundry.common.jsptag;

import javax.servlet.http.HttpServletRequest;
import javax.servlet.jsp.tagext.BodyTagSupport;

/**
 * css输出标签
 * @author liuxi
 */
public class CssTag extends BodyTagSupport{
	private static final long serialVersionUID = 1L;
	
	/**
	 * css文件地址，必填
	 */
	private String file;
	
	public int doEndTag(){
		//<link href="<%=request.getContextPath()%>/statics/css/base.css" rel="stylesheet" type="text/css"/>
		StringBuffer strb = new StringBuffer();
		strb.append( "<link href=\"" );
		if( StaticFileConfig.IS_CONTEXTPATH ){
			strb.append( ( (HttpServletRequest)super.pageContext.getRequest() ).getContextPath() );
		}
		strb.append( StaticFileConfig.RES_PREFIX );
		strb.append( file );
		strb.append( "?t0=" );
		strb.append( StaticFileConfig.t0 );
		strb.append( "\" rel=\"stylesheet\" type=\"text/css\"/>" );
		try{
			pageContext.getOut().write( strb.toString() );
		}catch( Exception e ){
			
		}
        return SKIP_BODY;
	}

	public void setFile(String file) {
		this.file = file;
	}
	
}
