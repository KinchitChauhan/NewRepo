
   



 
	// Get the modal
	var modal = document.getElementById('wh_login');

	// When the user clicks anywhere outside of the modal, close it
	window.onclick = function (event) {
		if (event.target == modal) {
			modal.style.display = "none";
		}
	}


	function openlogin() {

		document.getElementById("wh_sign").style.display = "none";
		document.getElementById("wh_login").style.display = "block";




	}


	function closelogin_page() {
		document.getElementById("wh_login").style.display = "none";


	}

	function closelogin() {

		document.getElementById("wh_sign").style.display = "block";
		document.getElementById("wh_login").style.display = "none";


	}


	function openchatform() {
		document.getElementById("chatForm").style.display = "block";
	}

	function closechatform() {
		document.getElementById("chatForm").style.display = "none";
	}



 



 
	var modal1 = document.getElementById('wh_sign');

	// When the user clicks anywhere outside of the modal, close it
	window.onclick = function (te) {
		if (te.target == modal1) {
			modal1.style.display = "none";
		}
	}




 



 



	function openNav() {
		function whroomnav(w) {
			if (w.matches) { // If media query matches
				document.getElementById("wh_nav_icon").style.width = "270px";
				document.getElementById("wh_nav_icon").style.display = "block";
				document.getElementById("wh_nav_hid").style.display = "none";
				document.getElementById("wh_nav_help_hid").style.display = "none";
				document.getElementById("wh_nav_what_hid").style.display = "none";



			} else {
				document.getElementById("wh_nav_icon").style.width = "350px";
				document.getElementById("wh_nav_icon").style.display = "block";
				document.getElementById("wh_nav_hid").style.display = "none";
				document.getElementById("wh_nav_help_hid").style.display = "none";
				document.getElementById("wh_nav_what_hid").style.display = "none";


			}
		}

		var w = window.matchMedia("(max-width: 600px)")
		whroomnav(w) // Call listener function at run time
		w.addListener(whroomnav) // Attach listener function on state changes
	}

	function closeNav() {
		document.getElementById("wh_nav_icon").style.width = "0";
		document.getElementById("wh_nav_icon").style.right = "5%";
		document.getElementById("wh_nav_hid").style.display = "block";
		document.getElementById("wh_nav_help_hid").style.display = "block";
		document.getElementById("wh_nav_what_hid").style.display = "block";


	}

	function openNav2() {
		document.getElementById("wh_Sidenav2").style.width = "250px";
		document.body.style.backgroundColor = "rgba(0,0,0,0.4)";
	}

	function closeNav2() {
		document.getElementById("wh_Sidenav2").style.width = "0";

		document.body.style.backgroundColor = "white";
	}
 


 
	function openCity(evt, cityName) {
		var i, tabcontent, tablinks;
		tabcontent = document.getElementsByClassName("tabcontent");
		for (i = 0; i < tabcontent.length; i++) {
			tabcontent[i].style.display = "none";
		}
		tablinks = document.getElementsByClassName("tablinks");
		for (i = 0; i < tablinks.length; i++) {
			tablinks[i].className = tablinks[i].className.replace(" active", "");
		}
		document.getElementById(cityName).style.display = "block";
		evt.currentTarget.className += " active";
	}

	// Get the element with id="defaultOpen" and click on it
	document.getElementById("defaultOpen").click();
 


 
	function openPage(pageName, elmnt, color) {
		var i, tabcontent, tablinks;
		tabcontent1 = document.getElementsByClassName("wh_infotabcontent");
		for (i = 0; i < tabcontent1.length; i++) {
			tabcontent1[i].style.display = "none";
		}
		tablinks1 = document.getElementsByClassName("wh_infolink");
		for (i = 0; i < tablinks1.length; i++) {
			tablinks1[i].style.backgroundColor = "";
		}
		document.getElementById(pageName).style.display = "block";
		elmnt.style.backgroundColor = color;

	}

	// Get the element with id="defaultOpen" and click on it
	document.getElementById("defaultinfoOpen").click();
 



 

	const wrapper = document.querySelector(".wh_wrapper");
	const carousel = document.querySelector(".wh_carousel");
	const firstCardWidth = carousel.querySelector(".wh_card").offsetWidth;
	const arrowBtns = document.querySelectorAll(".wh_arrow");
	const carouselChildrens = [...carousel.children];

	let isDragging = false, isAutoPlay = true, startX, startScrollLeft, timeoutId;

	// Get the number of cards that can fit in the carousel at once
	let cardPerView = Math.round(carousel.offsetWidth / firstCardWidth);

	// Insert copies of the last few cards to beginning of carousel for infinite scrolling
	carouselChildrens.slice(-cardPerView).reverse().forEach(card => {
		carousel.insertAdjacentHTML("afterbegin", card.outerHTML);
	});

	// Insert copies of the first few cards to end of carousel for infinite scrolling
	carouselChildrens.slice(0, cardPerView).forEach(card => {
		carousel.insertAdjacentHTML("beforeend", card.outerHTML);
	});

	// Scroll the carousel at appropriate postition to hide first few duplicate cards on Firefox
	carousel.classList.add("no-transition");
	carousel.scrollLeft = carousel.offsetWidth;
	carousel.classList.remove("no-transition");

	// Add event listeners for the arrow buttons to scroll the carousel left and right
	arrowBtns.forEach(btn => {
		btn.addEventListener("click", () => {
			carousel.scrollLeft += btn.id == "left" ? -firstCardWidth : firstCardWidth;
		});
	});

	const dragStart = (e) => {
		isDragging = true;
		carousel.classList.add("dragging");
		// Records the initial cursor and scroll position of the carousel
		startX = e.pageX;
		startScrollLeft = carousel.scrollLeft;
	}

	const dragging = (e) => {
		if (!isDragging) return; // if isDragging is false return from here
		// Updates the scroll position of the carousel based on the cursor movement
		carousel.scrollLeft = startScrollLeft - (e.pageX - startX);
	}

	const dragStop = () => {
		isDragging = false;
		carousel.classList.remove("dragging");
	}

	const infiniteScroll = () => {
		// If the carousel is at the beginning, scroll to the end
		if (carousel.scrollLeft === 0) {
			carousel.classList.add("no-transition");
			carousel.scrollLeft = carousel.scrollWidth - (2 * carousel.offsetWidth);
			carousel.classList.remove("no-transition");
		}
		// If the carousel is at the end, scroll to the beginning
		else if (Math.ceil(carousel.scrollLeft) === carousel.scrollWidth - carousel.offsetWidth) {
			carousel.classList.add("no-transition");
			carousel.scrollLeft = carousel.offsetWidth;
			carousel.classList.remove("no-transition");
		}

		// Clear existing timeout & start autoplay if mouse is not hovering over carousel
		clearTimeout(timeoutId);
		if (!wrapper.matches(":hover")) autoPlay();
	}

	const autoPlay = () => {
		if (window.innerWidth < 800 || !isAutoPlay) return; // Return if window is smaller than 800 or isAutoPlay is false
		// Autoplay the carousel after every 2500 ms
		timeoutId = setTimeout(() => carousel.scrollLeft += firstCardWidth, 8000);
	}
	autoPlay();

	carousel.addEventListener("mousedown", dragStart);
	carousel.addEventListener("mousemove", dragging);
	document.addEventListener("mouseup", dragStop);
	carousel.addEventListener("scroll", infiniteScroll);
	wrapper.addEventListener("mouseenter", () => clearTimeout(timeoutId));
	wrapper.addEventListener("mouseleave", autoPlay);
 

 
	// Get the button
	let mybutton = document.getElementById("myBtn_top");

	// When the user scrolls down 20px from the top of the document, show the button
	window.onscroll = function () { scrollFunction() };

	function scrollFunction() {
		if (document.body.scrollTop > 20 || document.documentElement.scrollTop > 20) {
			mybutton.style.display = "block";
		} else {
			mybutton.style.display = "none";
		}
	}

	// When the user clicks on the button, scroll to the top of the document
	function topFunction() {
		document.body.scrollTop = 0;
		document.documentElement.scrollTop = 0;
	}
 