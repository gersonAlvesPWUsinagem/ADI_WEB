(function () {
    function findParent(element, attributeName) {
        while (element && element !== document) {
            if (element.getAttribute && element.getAttribute(attributeName) !== null) {
                return element;
            }
            element = element.parentNode;
        }
        return null;
    }

    function closeAllMenus(exceptMenu) {
        var menus = document.querySelectorAll('[data-adi-mobile-menu]');
        var index;
        for (index = 0; index < menus.length; index++) {
            if (menus[index] !== exceptMenu) {
                menus[index].classList.remove('adi-mobile-nav--open');
                var button = menus[index].querySelector('[data-adi-mobile-menu-button]');
                if (button) {
                    button.setAttribute('aria-expanded', 'false');
                }
            }
        }
    }

    document.addEventListener('click', function (event) {
        var button = findParent(event.target, 'data-adi-mobile-menu-button');
        if (button) {
            event.preventDefault();
            var menu = findParent(button, 'data-adi-mobile-menu');
            var isOpen = menu.classList.contains('adi-mobile-nav--open');
            closeAllMenus(menu);
            if (isOpen) {
                menu.classList.remove('adi-mobile-nav--open');
            } else {
                menu.classList.add('adi-mobile-nav--open');
            }
            button.setAttribute('aria-expanded', isOpen ? 'false' : 'true');
            return;
        }

        if (!findParent(event.target, 'data-adi-mobile-menu')) {
            closeAllMenus(null);
        }
    }, false);
})();
