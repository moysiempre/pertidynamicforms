function collapseNavbar() {
    if ($("#main-menu").offset().top > 50) {
        $("#main-menu").addClass("top-nav-collapse");        
    } else {
        $("#main-menu").removeClass("top-nav-collapse");
    }
}

	$(window).scroll(collapseNavbar);
	$(document).ready(collapseNavbar);