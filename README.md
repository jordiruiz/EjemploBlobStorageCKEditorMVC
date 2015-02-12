# EjemploBlobStorageCKEditorMVC
SUBIR IMÁGENES A AZURE BLOB SOTARGE DESDE CKEDITOR EN UNA WEB ASP.NET MVC 5

1.	Añadimos a la página el script de CKEditor desde su CDN
2.	Creamos un textarea que será reemplazado por CKEditor
3.	Decimos a CKEditor el nombre de nuestro textarea
4.	Configuramos la url de la página que CKEditor utilizará para el listado de imágenes subidas al Azure Blob Storage 
5.	Configuramos la url de la acción que recibirá la imagen a subir al Azure Blob Storage
 
6.	Creamos el modelo CKImageViewModel para ser usado por la vista del listado de imágenes
 
7.	Creamos el controlador ckeditor con dos métodos
a.	Index: Recuperará las imágenes
b.	Uploadnow: Recibe la imagen a subir

 
8.	Creamos dos vistas:
a.	Index: Mostrara un listado de imágenes
 
b.	UploadNow: Mostrará un texto con el resultado de la operación de subida de una imagen.
 

9.	Crear una cuenta de almacenamiento de Azure.
 
 
Cada cuenta de almacenamiento puede tener uno o varios contenedores, podemos crear un contenedor desde el propio azure o crearlo por código en caso de que no exista, como veremos a continuación.
10.	Instalamos el SDK de Azure para Visual Studio 2013.
Para instalarlo podéis hacerlo desde “Web Platform Installer” 
 
11.	Iniciar el emulador de  almacenamiento de Azure (Azure Storage Emulator)
Con el Azure web role, se puede configurar para que inicie automáticamente el emulador pero para el ejemplo lo ejecutaremos manualmente, para la versión del SDK 2.5 Visual Studio 2013 el comando seria:
“C:\Program Files (x86)\Microsoft SDKs\Azure\Storage Emulator\WAStorageEmulator.exe  start”
 
También se puede automatizar por código este proceso
12.	Añadir una cadena de conexión:
Mientras estamos desarrollando podemos utilizar el emulador que acabamos de iniciar en el paso anterior y añadir una cadena de conexión en el web.config de esta forma:
<connectionStrings>
  <add name="AzureStorageConnection" connectionString="UseDevelopmentStorage=true"/>
</connectionStrings>
13.	Instalar el paquete de nuget de WindowsAzure.Storage

 
14.	En el controlador que hemos creado “ckeditorController” añadimos los namespaces de Azure y el System.Configuration para acceder a la connectionString
 

15.	Conectamos con la cuenta de almacenamiento de Azure
Creamos el contenedor en caso de que no exista y en este caso le damos acceso público. (El nombre del contenedor debe ser en minúsculas y sin espacios sino provoca una excepción)
16.	Configuramos el nombre del blob que será la ruta de la imagen. Aquí podremos incluir carpetas para separar las imágenes simulando el sistema de archivos de Windows.
17.	Finalmente subimos la imagen o sobrescribimos en caso de que exista.  

 

18.	Obtener las imágenes del contenedor especificando la ruta a partir del directorio anterior.
 
19.	Para el pasa a producción sustituimos la cadena de conexión:

<!--<add name="AzureStorageConnection" connectionString="UseDevelopmentStorage=true" />-->
    <add name="AzureStorageConnection" connectionString="DefaultEndpointsProtocol=https;AccountName=NombreCuenta;AccountKey=ClaveCuenta" />
 
