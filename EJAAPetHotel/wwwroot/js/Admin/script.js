const d = document,
      w = window;


w.addEventListener("DOMContentLoaded", () =>{
    mobileMenu();
    showIndexModal();
    showFormModal();
    navItemAnimation();
    goBack();
    disableDates();
    updateMaintenance();
});

const updateMaintenance = () =>{
    var autoSubmitInputs = document.getElementsByClassName("auto-submit");

    for (var i = 0; i < autoSubmitInputs.length; i++) {
        autoSubmitInputs[i].addEventListener("change", function () {
            var form = this.closest("form");
            form.submit();
        });
    }
}


const showIndexModal = () => {
    const $rowList = d.querySelectorAll('.index__table__wrapper .table__row')

    if ($rowList) {
        $rowList.forEach(x => {
            let dataTargetValue = x.getAttribute('data-target'),
                $index__modal__wrapper = d.getElementById(dataTargetValue);

            d.addEventListener('click', e => {

                if (e.target === x) {
                    $index__modal__wrapper.classList.remove('hidden');
                    $index__modal__wrapper.firstElementChild.classList.add('showModal');
                }
            });

            const $closeBtn = $index__modal__wrapper.querySelector('.closeBtn');
            $closeBtn.addEventListener('click', () => {
                $index__modal__wrapper.classList.add('hidden');
                $index__modal__wrapper.firstElementChild.classList.remove('showModal');

            });
        });
    }
}

//add new employee or maintenance
const showFormModal = () => {

    const $modalContainer = d.querySelector('.modal__wrapper'),
        $modal = d.querySelector('.modal__wrapper .modal'),
        $addBtn = d.querySelector('.btnAdd'),
        $closeBTn = d.querySelector('.closeBtn');

    d.addEventListener('click', (e) => {

        if (e.target === $addBtn) {
            $modalContainer.classList.remove('hidden');
            $modal.classList.add('showModal');
        };

        if (e.target === $closeBTn) {
            $modalContainer.classList.add('hidden');
            $modal.classList.remove('showModal');
        }
    });
}


const navItemAnimation = () => {
    const currentPage = window.location.pathname.toLowerCase(),
          $navLinks = document.querySelectorAll('.menu__item a'),
          $navLinksMobile = document.querySelectorAll('.mobile__menu__item a');

    $navLinks.forEach(x => {
        const linkHref = x.getAttribute('href').toLowerCase(),
              parentMenuItem = x.closest('.menu__item');

        (linkHref === currentPage) ? parentMenuItem.classList.add('is-active') : parentMenuItem.classList.remove('is-active');
    });

    $navLinksMobile.forEach(x => {
        const linkHref = x.getAttribute('href').toLowerCase(),
              parentMenuItemMobile = x.closest('.mobile__menu__item');

        (linkHref === currentPage) ? parentMenuItemMobile.classList.add('is-active') : parentMenuItemMobile.classList.remove('is-active');
    });
};

// show mobile menu 
const mobileMenu = () => {
    const $menuPanel = d.querySelector('.mobile__menu'),
     $hamburgerBtn = d.querySelector('.hamburgerBtn i'),
     $closeHamburgerBtn = d.querySelector('.closeMenuBtn i');

    d.addEventListener('click', (e) =>{

        if(e.target === $hamburgerBtn){
            $menuPanel.classList.add('open');
        }
        
        if(e.target === $closeHamburgerBtn ){
            $menuPanel.classList.remove('open');
        }
    });
}

const goBack = () =>{
    const $goBackBtn = d.querySelector('#goBack');
    d.addEventListener('click', (e) =>{
        if(e.target === $goBackBtn){
            console.log('atraas')
            history.back();
        }
    });
}

//Disable Previous Dates in input Date
const disableDates = () =>{
    const $dateTimeSelector = d.querySelector('input[type="datetime-local"]');

    if($dateTimeSelector){
        let currentDate = new Date().toISOString().slice(0, 16);
        $dateTimeSelector.min = currentDate;
    }
}




