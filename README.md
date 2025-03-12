## Anteproyecto - Catálogo Inteligente de Videojuegos con IA y Recomendaciones Personalizadas

### **Plataformas**
- Web

### **Tecnologías**
- **Backend:** ASP.NET Core (C#), Entity Framework Core
- **Frontend:** Angular (TypeScript) con Firebase Hosting
- **Bases de Datos:** PostgreSQL
- **Despliegue:** Docker Compose en Oracle Cloud VPS
- **Inteligencia Artificial:** ML.NET o TensorFlow para recomendaciones, Modelos NLP para análisis de reseñas

## 1. INTRODUCCIÓN
El mundo de los videojuegos cuenta con una enorme cantidad de títulos disponibles, lo que hace que encontrar un juego adecuado según los gustos del usuario sea un desafío. Este proyecto busca desarrollar una plataforma de recomendación de videojuegos donde los usuarios puedan explorar, calificar y recibir sugerencias personalizadas. Además, la plataforma integrará una IA que recomendará juegos en función de los intereses del usuario y proporcionará tips y ayuda en los títulos en los que está jugando. Para enriquecer el catálogo, también se incluirán videos de YouTube oficiales del juego, así como gameplays con mayores visualizaciones.

## 2. OBJETIVOS
### **Objetivo General**
Desarrollar una aplicación web que permita a los usuarios explorar videojuegos por categorías, notas y filtros avanzados, con funciones de recomendación basada en IA, acceso a noticias y compras en tiendas externas, e integración de contenido multimedia.

### **Objetivos Específicos**
1. Desarrollar una API RESTful en ASP.NET Core (C#) que permita la consulta y filtrado de videojuegos por categorías, géneros, plataformas y puntuaciones.
2. Implementar PostgreSQL como base de datos principal para almacenar información sobre los videojuegos, opiniones y preferencias de los usuarios.
3. Implementar un sistema de autenticación con Firebase Authentication o JWT para permitir el registro e inicio de sesión de usuarios.
4. Diseñar un sistema de puntuación y comentarios donde los usuarios puedan evaluar los juegos jugados.
5. Desarrollar una IA de recomendación de videojuegos que sugiera títulos similares según el historial y preferencias del usuario.
6. Incluir un sistema de ayuda con IA que brinde tips y estrategias basadas en la experiencia del jugador.
7. Integrar un feed de noticias sobre videojuegos con información actualizada.
8. Implementar enlaces a tiendas externas para facilitar la compra de videojuegos desde la plataforma.
9. Incorporar videos de YouTube oficiales y gameplays populares dentro de la plataforma para enriquecer la experiencia del usuario.
10. Desplegar la aplicación usando Docker Compose en un servidor VPS de Oracle Cloud.

## 3. DESCRIPCIÓN DEL PROYECTO
La plataforma consistirá en una aplicación web moderna que permitirá a los usuarios explorar y descubrir videojuegos de manera eficiente. Se centrará en la experiencia del usuario, ofreciendo herramientas avanzadas de búsqueda, recomendaciones personalizadas con IA y contenido multimedia enriquecido.

### **3.1. Módulos Principales**

#### **Exploración y Búsqueda**
- Filtros avanzados para buscar juegos por categoría, plataforma, fecha de lanzamiento y calificación.
- Búsqueda rápida mediante un sistema predictivo basado en IA.
- Listado de juegos más populares y mejor valorados por la comunidad.

#### **Recomendaciones con IA**
- Algoritmo de aprendizaje automático para sugerir videojuegos basados en el historial y preferencias del usuario.
- Análisis de tendencias y preferencias de otros jugadores con perfiles similares.
- Sección de "Juegos que podrías disfrutar" personalizada.

#### **Sistema de Reseñas y Valoraciones**
- Los usuarios podrán calificar los juegos jugados con un sistema de puntuación.
- Sección para dejar opiniones y leer comentarios de otros jugadores.
- Sistema de validación de reseñas para destacar las más útiles.

#### **Noticias y Actualizaciones**
- Sección de noticias con información sobre próximos lanzamientos, actualizaciones y eventos especiales en la industria del videojuego.
- Integración con fuentes externas para mantener contenido actualizado.

#### **Integración de Videos y Gameplays**
- Visualización de tráilers y videos promocionales de videojuegos.
- Reproducción de gameplays populares con alta relevancia en plataformas como YouTube.

#### **Acceso a Tiendas Externas**
- Comparación de precios entre diferentes plataformas de compra digital.
- Enlace directo a tiendas como Steam, Epic Games Store, PlayStation Store, Xbox Store y Nintendo eShop.

#### **Autenticación y Gestión de Usuarios**
- Registro e inicio de sesión utilizando Firebase Authentication o JWT.
- Perfiles de usuario con juegos jugados y lista de favoritos.
- Configuración de preferencias para mejorar las recomendaciones personalizadas.

## 4. INFRAESTRUCTURA DEL PROYECTO
Para garantizar un despliegue eficiente, seguro y escalable del catálogo de videojuegos, la infraestructura se basará en un **VPS con Ubuntu Server LTS** y se compondrá de múltiples contenedores gestionados con **Docker Compose**. La arquitectura propuesta incluye los siguientes componentes:

### **4.1. Servidor VPS**
- Ubuntu Server LTS.
- Firewall configurado para restringir accesos innecesarios.
- Acceso remoto seguro mediante **SSH**.

### **4.2. Contenedores y Servicios**
Se utilizará **Docker Compose** para la gestión de los siguientes servicios:

#### **Frontend**
- Aplicación Angular desplegada en **Firebase Hosting**.

#### **Backend**
- API REST desarrollada en **ASP.NET Core**.
- Base de datos relacional **PostgreSQL**.
- **Weaviate** para potenciar búsquedas vectoriales y mejorar recomendaciones.
- Servidor web **Nginx** para gestionar las peticiones al backend.

#### **Load Balancer y Reverse Proxy**
- **Nginx** se utilizará como balanceador de carga y proxy inverso para distribuir las solicitudes correctamente entre los servicios.

#### **Integración con APIs externas**
- Conexión con la API de **IGDB** para obtener información de videojuegos y enriquecer el catálogo.

### **4.3. Seguridad y Administración**
- Restricción de acceso mediante **firewall**.
- Uso de **Firebase Authentication o JWT** para autenticación de usuarios.
- Implementación de **roles y permisos** en la plataforma.

### **4.4. Monitorización y Logs**
- **Prometheus + Grafana** o **ELK Stack (Elasticsearch, Logstash, Kibana)** para la monitorización del sistema y gestión de logs.

### **4.5. Despliegue y Automatización**
- **CI/CD con GitHub Actions o GitLab CI/CD** para automatizar el despliegue.
- **Docker Swarm o Kubernetes** como opción futura para escalabilidad.

## 5. RESULTADOS ESPERADOS
- Plataforma funcional donde los usuarios puedan encontrar, filtrar y descubrir videojuegos.
- Algoritmo de IA capaz de recomendar juegos según los gustos del usuario.
- Aplicación web con una interfaz moderna e intuitiva.
- Implementación de noticias, acceso a tiendas externas y videos de YouTube para enriquecer la experiencia del usuario.
- Despliegue de la aplicación en un entorno escalable mediante Docker Compose en Oracle Cloud VPS.

## 6. CONCLUSIÓN
Este proyecto ofrecerá a los jugadores una herramienta completa para explorar el mundo de los videojuegos, mejorando su experiencia con recomendaciones inteligentes y contenido relevante. La implementación de inteligencia artificial permitirá sugerencias personalizadas y la integración con múltiples fuentes garantizará una experiencia enriquecida para los usuarios.

