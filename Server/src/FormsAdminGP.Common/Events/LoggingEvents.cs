﻿namespace FormsAdminGP.Common.Events
{
    public class LoggingEvents
    {
        #region COMMON
        public const string PROCESS_SUCCESS_MESSAGE = "El proceso se realizó con éxito";
        public const string PROCESS_FAILED_ADMIN = "no se pudo completar el proceso, favor de contactar el administrador";

        public const string INSERT_SUCCESS_MESSAGE = "El registro se guardó con éxito";
        public const string INSERT_FAILED_MESSAGE = "No se pudo guardar el registro correspondiente";
        public const string INSERT_DUPLICATED_MESSAGE = "No se pudo guardar, el sistema ya cuenta con un registro con ese nombre";

        public const string UPDATE_SUCCESS_MESSAGE = "El registro se actualizó con éxito";
        public const string UPDATE_FAILED_MESSAGE = "No se pudo actualizar los datos correspondiente";
        public const string DELETE_SUCCESS_MESSAGE = "El registro se eliminó con éxito";
        public const string DELETE_FAILED_MESSAGE = "No se pudo eliminar el registro correspondiente";
        public const string MODELSTATE_INVALID_MESSAGE = "No se pudo completar el proceso, favor de validar los campos";

        public const string UPLOAD_FILE_SUCCESS_MESSAGE = "El archivo fue cargado con éxito";
        public const string UPLOAD_FILE_FAILED_MESSAGE = "No se pudo cargar el archivo";
        public const string REMOVE_FILE_SUCCESS_MESSAGE = "El archivo fue removido con éxito";
        public const string REMOVE_FILE_FAILED_MESSAGE = "No se pudo remover el archivo";

        public const string LOGIN_FAILED_MESSAGE = "Usuario o contraseña incorrecto";
        public const string USER_FAILED_MESSAGE = "Usuario no encontrado en el sistema";
        public const string RESETE_FAILED_MESSAGE = "No se pudo resetear tu contraseña";
 

        #endregion
    }
}
