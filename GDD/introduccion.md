# Introducción
O xogo tratarase dun "autorunner", é dicir, un xogo no que o xogador avanzará nunha dirección automáticamente. O xogador terá que evitar obstáculos que se aproximarán a él saltando ou agachándose. O público obxectivo será calquera persona que teña 5 minutos libres para gastar nunha experiencia simple pero divertida. O rango de idade serán personas de 3 a 99 anos.

## Descripción
+ Xénero: Plataformas/autorunner
+ Obxectivos: Ganar puntos ao sobrevir o maior tempo posibé sin que as vidas do xogador cheguen a cero por mor de chocar con obstáculos.
+ Mecánicas: O xogador pode saltar e agocharse para evitar obstáculos. Tamén haberá un sistema de vidas e de puntuación.

## Trasfondo
Este xogo estaría inspirado por xogos como Geometry Dash ou o Dinosaur Game (o xogo que aparece cando te quedas sin conexión a internet en google chrome).

## Mecánicas
- Xogador
  + Salto: O xogador podrá saltar para evitar verticalmente os obstáculos. O salto ten unha alta velocidade e unha altura fixa.
  + Agacharse: O xogador poderá agacharse para reducir a súa altura e evitar obstáculos que se dirixan á parte alta do xogador
- Obstáculos
    + Primeiro nivel: Haberá osbstáculos que aparecerán á mesma altura co personaxe xogador, que se evitarán saltando por encima deles.
    + Segundo nivel: Haberá obstáculos que se dirixirán á parte superior do xogador, polo que este deberá agacharse ou saltar sobre eles.
    + Terceiro nivel: Estes obstáculos aparecerán encima do xogador, castigandoo no caso no que saltase nun mal momento.
- Nivel
  + En cuestión de programación, o xogador non se desplaza horizontalmente, senon que o escenario se move en dirección do xogador. O nivel repetirase indifinidamente ata que o xogador perda todas as súas vidas.
  + A velocidade de desplazamento aumentará cada certo intervalo de tempo [*EDITAR CO TEMPO EXACTO*], e polo tanto a dificultade do xogo tamén aumentará.
- Puntuación
  + O xogador acumulará puntos durante o tempo que se manteña con vida.
  + Canto máis aumente a velocidade do xogo máis puntos se ganarán por segundo.