document.addEventListener('DOMContentLoaded', function() {
    const carouselSlides = document.querySelectorAll('.carousel-slide');
    let currentSlide = 0;

    function showSlide() {
    carouselSlides.forEach(function(slide) {
        slide.classList.remove('active');
    });
    carouselSlides[currentSlide].classList.add('active');
    currentSlide = (currentSlide + 1) % carouselSlides.length;
    }

    setInterval(showSlide, 3000);
});
