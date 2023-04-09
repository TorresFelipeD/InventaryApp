# TITULO DEL DESIGN DOC
Link: [Design-Doc-InventaryApp](https://github.com/TorresFelipeD/InventaryApp/blob/v0.0.0-alpha.1/docs/design-doc.md)

Author(s): Diego Chaparro

<!-- Status: [Draft, Ready for review, In Review, Reviewed] -->
Status: Draft

Ultima actualización: 2023-04-09

---
## __Contenido__
- Goals
- Non-Goals
- Background
- Overview
- Detailed Design
  - Solucion
- Consideraciones
- Métricas

---
## __Links__
<!-- - [Un link](#) -->
- !=

---
## __Objetivo__

Una aplicación web para registrar el inventario de una alacena familiar, con el fin de generar un control de los alimentos, y asi mismo una recomendación de menús

---
## __Goals__

- Registro de productos de alacena
- Notificación de fecha de caducidad por correo o cualquier medio de alerta
- Registro de productos esenciales o favoritos
    - Notificación de faltante o adquisición de reserva
- Recomendación de menu con lo que hay
- Sugerencia de recetas
- Lista de pendientes
    - Notificación de esta lista

---
## __Non-Goals__
- No será una aplicación de domicilios

---
## __Background__
Con el fin de llevar un control de los alimentos que se compraban y asi poder revisar que realmente es util y que menus se pueden hacer con lo que se tiene surgio la idea

---
## __Overview__
Se requiere construir una aplicación web, que le permita al usuario agregar los detalles del producto. También que pueda añadir menus de forma manual con los items de los productos actuales, debe existir un menu de lista de pendientes por si dentro del menu construido se requiere un producto adicional.

El sistema debe tener autenticación usando JWT.

El backend y el frontend deben estar separados.

La tecnologia inicial para el backend sera en .net 6 y para el front sera en Angular.

Se espera que se implemente una arquitectura de microservicios para el backend, y se intentara que haya mas de un lenguaje como python, nodejs y java, esto es opcional puesto que el lenguaje principal será c#.

Para la generación de menus, estos existiran de forma global en la aplicación, es decir no sera dependiente de usuarios, esto con el fin de dar recomendaciones cada vez que se cree una nueva receta. 

Adicional usando web scrapping se buscara generar recomendaciones adicionales. Aunque esto es un alcance para futuras versiones.

---
## __Detailed Design__

### __Solución__
A continuación se relacionan los modulos que deben existir:

__Autenticación__

Registro por usuario y contraseña, roles (admin, moderator, customer)

__Inventario__

Todo el registro de productos, esto debe ir por nombre un id, a que usuario le pertenece, una descripción, una imagen, fecha de compra y fecha de vencimiento. Marcación de producto escencial, creación de lista de pendientes. Porcentaje de consumo.

__Generacion de Menus__

Usando los productos actuales debe generar menus, y que estos sean modificables, seria necesario realizar una revisión para no repetir menus de forma exacta con diferentes ids, en el caso de que el menu exista la modificación debe crear uno nuevo para realizar la modificación

__Generacion de Menus Online__

Este servicio será el de web scrapping, debe tener una monitorización por si hay alguna validación por browser o una caida de pagina, hay que analizar la construcción del menu bajo estructuras modificables desde el front, aunque este debe ser solo visible para su alteración desde un rol administrador o moderador.

__Notificaciones por correo__

Ya sea por que un producto este proximo a vencer, no tenga reserva, haya sido marcado como escencial deberá ejecutar una notificación por correo

---
## __Consideraciones__
- Inicialmente el backend debe ser en C# y el front en Angular. Las paginas para web scrapping pueden fallar, se debe tener contemplado una herramienta de monitorización.

## __Métricas__
- Usando librerias de logging para analizar el rendimiento y comportamiento final de los diferentes modulos
