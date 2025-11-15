namespace LearningPhase2.AppCode
{
    public static class CommonFunction
    {
        public static List<string> GetDepartments()
        {
            List<string> items = new List<string>();
            items.Add(Constants.ITDepartment);
            items.Add(Constants.HRDepartment);
            items.Add(Constants.PayRollDepartment);
            items.Add(Constants.LeaveDepartment);
            items.Add(Constants.AttendanceDepartment);
            return items;
        }

        public static List<string> GetStatusList()
        {
            List<string> items = new List<string>();
            items.Add(Constants.Active);
            items.Add(Constants.InActive);

            return items;
        }
    }
}
