var scrollNav;
function loaded() {
	scrollNav = new iScroll('sidebar-wrapper',{vScrollbar:false});
}
document.addEventListener('DOMContentLoaded', loaded, false);