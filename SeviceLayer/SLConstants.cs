namespace SeviceLayer
{
    public static class SLConstants
    {
        public static class Messages
        {
            public const string EmployeeAddedSuccesMessage = "Employee has been added successfully";
            public const string EmployeeAddedFailureMessage = "Record cannot be added Email Id should be unique";
            public const string EmployeeUpdatedSuccesMessage = "Employee has been Updated successfully";
            public const string EmployeeUpdatedFailureMessage = "Record cannot be Updated Email Id should be unique";
            public const string EmployeeDeletedSuccesMessage = "Employee has been Deleted successfully";
            public const string EMPLOYEE_ADDED_FAILURE_NAME_UNIQUE = "Employee cannot be added as name should be unique";
            public const string EMPLOYEE_ADDED_FAILURE_PHONE_LENGTH = "Employee cannot be added as PhoneNumber should not exceed 12 digits";
            public const string DEPARTMENT_ADDED_SUCCESS_MESSAGE = "Department has been added successfully";
            public const string DEPARTMENT_ADDED_FAILURE_MESSAGE = " Department cannot be added Name Should be unique";
            public const string DEPARTMENT_DELETED_SUCCESS_MESSAGE = "Department has been deleted successfully";
            public const string DEPARTMENT_UPDATED_SUCCESS_MESSAGE = "Department has been updated successfully";
            public const string DEPARTMENT_DELETED_FAILURE_MESSAGE = "Department cannot be updated successfully Name is not unique";
            public const string INVALID_LOGIN_MESSAGE = "Incorrect EmailId or Password";
            public const string EDUCATIONDETAILS_ADDED_SUCCESS_MESSAGE = "Education details has been added successfully";
            public const string EDUCATIONDETAILS_ADDED_FAILURE_MESSAGE = "Education details cannot be added successfully";
            public const string EDUCATIONDETAILS_UPDATED_SUCCESS_MESSAGE = "Education details has been updated successfully";
            public const string EDUCATIONDETAILS_UPDATED_FAILURE_MESSAGE = "Education details cannot be updated";
            public const string SESSION_EXPIRED_MESSAGE = "Session Expired";
            public const string RECORD_NOT_FOUND_MESSAGE = "This record is not found!";
            public const string EDUCATIONDETAILS_DELETED_SUCCESS_MESSAGE = "Education details has been deleted";
            public const string EDUCATIONDETAILS_DELETED_FAILURE_MESSAGE = "Education details cannot be deleted ";
            public const string EMPLOYEE_NOT_FOUND_MESSAGE = "Employee not found for this particular company";
        }
    }
}
