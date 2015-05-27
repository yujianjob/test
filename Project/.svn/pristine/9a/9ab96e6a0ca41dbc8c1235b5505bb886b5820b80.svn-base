package com.landaojia.washclothes.laundry.common.jsptag;

import javax.servlet.http.HttpServletRequest;
import javax.servlet.jsp.tagext.BodyTagSupport;

/**
 * javascript输出标签
 * @author liuxi
 */
public class JsTag extends BodyTagSupport{
	private static final long serialVersionUID = 1L;
	
	/**
	 * js文件地址，必填
	 */
	private String file;
	
	public int doEndTag(){
		//<script src="<%=request.getContextPath()%>/statics/js/base.js" type="text/javascript"></script>
		StringBuffer strb = new StringBuffer();
		strb.append( "<script src=\"" );
		if( StaticFileConfig.IS_CONTEXTPATH ){
			strb.append( ( (HttpServletRequest)super.pageContext.getRequest() ).getContextPath() );
		}
		strb.append( StaticFileConfig.RES_PREFIX );
		strb.append( file );
		strb.append( "?t0=" );
		strb.append( StaticFileConfig.t0 );
		strb.append( "\" type=\"text/javascript\"></script>" );
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
