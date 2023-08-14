const rotateButton = document.getElementsByClassName('navbar-toggler-custom')[0];

rotateButton.addEventListener('click', () => {
  const iconElement = rotateButton.querySelector('i');
  iconElement.classList.add('rotate');
  
  // Eliminamos la clase después de la animación (2 segundos)
  setTimeout(() => {
    iconElement.classList.remove('rotate');
  }, 2000);
});