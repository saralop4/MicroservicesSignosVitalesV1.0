# MicroservicesSignosVitalesV1.0
Cree este microservicio para un software de salud con HU I4 - Escala Obstétrica de alerta temprana

Actualmente el sistema no cuenta con ninguna alarma visual sobre los signos vitales del paciente como lo solicita la resolución 3280 del 2018

Yo

Como Clínica Salud Social

Quiero contar con una alerta en el censo del paciente

Para llevar el control del estado de salud actual

 
Por lo anterior se requiere adicionar un campo en la interfaz de signos vitales, notas de enfermería y evoluciones fijas / dinámicas llamado estado de conciencia, el cual contendrá (Alerta/No Alerta)

    Signos Vitales

imagen1

    1.      Notas de Enfermería


Imagen2


       Evoluciones fija y dinámicas


Imagen3


Además, una barra en el módulo censo hospitalario como se muestra en la imagen4, la cual puede ser de color Rojo, Naranja, amarillo o blanco según la sumatoria de los parámetros que se obtiene de cada uno de los módulos mencionados anteriormente (Imagen1,2 y 3). En la imagen5 podrán apreciar los valores a calcular.

 
Imagen4


Imagen5

La sumatoria de parámetros mencionada anteriormente se hace según la categoría de cada signo vital, por ejemplo, si la presión Sistólica es <80 su puntuación es 3, si la presión Diastolica es 90-99 su puntuación es 1, si la frecuencia respetaría es 25-29 su puntuación es 2, asi sucesivamente con cada signo vital.

 

Luego de tener la puntuación de cada signo vital se realiza una suma total y dependiendo ese valor se asigna color a la barra adicionada en el censo (Imagen 6)



Además, se debe adicionar un Tooltip al momento de pasar el mause por la alerta (Barra de color) que indique lo siguiente:

 

·        Blanco: Observación de Rutina

·        Amarillo: Llamado a enfermera a cargo

·        Naranja: Llamado urgente a equipo medico

·        Rojo: Llamado emergente al equipo cuidado critico

