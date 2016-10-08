# CrioGame

La idea tras *CrioGame* era crear un motor de videojuegos aprovechando las librerías de *MonoGame* pero
añadiendo las funcionalidades más básicas de los juegos como las hojas de sprites, el tratamiento de colisiones,
la lógica de vistas, etc...

La idea inicial de *CrioGame* es separar la lógica del videojuego de las capas de acceso a la tarjeta gráfica, por
eso se separa el proyecto en tres librerías:

* CrioGame.Common: define las interfaces y las estructuras básicas como los vectores, los modelos para dibujo, etc...
* CrioGame.GameEngine: define el motor del juego. En este proyecto se implementa por ejemplo la lógica de colisiones, 
los objetos para animaciones o fondos, las escenas, los controles de usuario...
* CrioGame.ImplMonogame: define la implementación gráfica. En este caso se utiliza MonoGame para el manejo de la tarjeta
gráfica, los controladores de entrada (ratón y teclado, por ahora) y el sonido.
Como se ve, la idea es separar la implementación del motor del juego de forma que si en un momento dado en lugar de MonoGame
queremos utilizar OpenTK , SharpDK , SDL o cualquier otra implementación gráfica, podamos simplemente cambiar 
el proyecto de implementación (en nuestro caso CrioGame.ImplMonogame ) por nuestro motor preferido.

Para conseguirlo, en la sección de Interfaces de CrioGame.GameEngine se identifican los diferentes interfaces 
que deben cumplir las diferentes capas del juego. Así:

* IGameEngineManager: agrupa los diferentes interfaces que debe cumplir el motor de videojuegos.
* IGraphicsEngineManager: agrupa las interfaces que debe cumplir la implementación gráfica, es decir, el manejo de tarjeta gráfica, sonido y controladores de entrada.

# Juegos de ejemplo

Como crear un motor de videojuegos no sirve de mucho a menos que tengamos algún juego de ejemplo, en la 
solución tenemos cuatro juegos clásicos:

* Mines: basada en el tutorial sobre MonoGame de Tara E. Walker con el que comencé a desarrollar el entorno.
* Arkanoid: una implementación sencilla del clásico Arkanoid.
* Asteroids: el clásico juego de asteroides.
* SpaceWars: ¿cómo podían faltar unos marcianitos?

Los cuatro juegos mantienen una estructura similar. El único detalle es que les he ido añadiendo funcionalidades, de
forma que Mines es el más sencillo, con una única escena mientras que SpaceWars tiene ya carga de recursos, diferentes
escenas y menús.

Por supuesto, los gráficos y sonidos no son míos, son ejemplos gratuitos descargados de Internet.

# ¿Punto final?
El motor del juego no está terminado del todo. Permite hacer juegos sencillos con animaciones, fondos
parallax, diferentes escenas... pero le falta mucho para ser un motor de creación de videojuegos completo.

Aunque nos puede servir como base para juegos sencillos sin demasiado esfuerzo, aún no estoy del todo contento
con algunos aspectos como el manejo de cámaras o sprites.
