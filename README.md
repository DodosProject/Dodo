To DoDo!

Se trata de una página web en la cual un usuario puede gestionar su listas de tareas (creando, completando y borrando las tareas según su criterio). 

El proyecto cuenta con una API que cuenta con operaciones CRUD:
Recoger listados de tareas del usuario
  * Modificar una tarea, marcándolo como tarea completada
  * No podrá ser marcado como “no completada” después de haber sido completado 
  * Añadir una nueva tarea a su listado de tareas
  * Borrar una tarea
Hemos contenerizado la API, web y BD con Docker Compose.
Para su diseño y maquetación hemos utilizado wireframes y Sass.

Este es el resumen de los requisitos realizados:

Requisitos obligatorios
  * Funcionalidad (API): Diseña e implementa una API con una correcta arquitectura RESTful, métodos HTTP para las operaciones CRUD y códigos de estado HTTP
  * Funcionalidad (web): La aplicación web debe listar el contenido de, al menos, un par de clases del modelo de datos en dos páginas diferentes. Además, deberá incluir algún tipo de mecanismo de autenticación (no obligatoriamente contra backend). Se deberá crear la aplicación mediante Vue 3, utilizar Pinia para la gestión centralizada del estado y, deberá utilizarse vue-router para la configuración de las rutas de la aplicación (incluidos los guards). 
  * DevOps: Conteneriza la API y la base de datos e integrales en el proyecto local Docker
  * Diseño: Realiza la maquetación del sitio usando Sass, con al menos un mixin y explica la elección de colores, fuentes y espacios del sitio web

Otras funcionalidades
  * Funcionalidad I: Añade funcionalidades de búsqueda que permita filtrar y ordenar los resultados por, al menos, un campo.
  * Funcionalidad II: Amplía la aplicación web para que sea posible registrar información (mediante formularios) y eliminar la existente. (CRUD, nuevos elementos y modificación de existentes)
  * Funcionalidad (API): Aplica buenas prácticas de programación como una arquitectura por capas e inyección de dependencias.
  * DevOps II: Ejecuta los contenedores conjuntamente mediante un orquestador de contenedores en tu entorno local: Docker Compose, Docker Swarm, Kubernetes
  * Diseño I: Creación de wireframes con sketchy wireframes (Figma) antes de empezar el desarrollo.
  * Inglés: Traduce todos los textos de la aplicación web para que se muestre completamente en inglés.

Para realizar el despliegue, por favor descarge el proyecto, situése en la carpeta backend y ejecute el siguiente comando:
    `docker-compose up -d`
