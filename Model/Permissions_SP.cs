namespace Model
{
    public class Permissions_SP
    {
        public const string GetListById = "cd89f1e0-cde8-4bfe-99e2-586a8a9d337e";
        public const string GetListByRouteId = "SP_GetListById";
        public const string GetListByDescription = "Get a list of shortlisted properties";

        public const string GetById = "d1b20213-7eb9-4816-a24d-21c34e117b06";
        public const string GetByRouteId = "SP_GetById";
        public const string GetByDescription = "Get a specific shortlisted property";

        public const string DeleteId = "87ca67be-5cac-4f63-ad9a-a7e6de9c9f85";
        public const string DeleteRouteId = "SP_Delete";
        public const string DeleteDescription = "Delete a shortlisted property";

        public const string UpdateId = "4337dc36-1fa2-41f1-a8d7-6f224df4aa31";
        public const string UpdateRouteId = "SP_Update";
        public const string UpdateDescription = "Update a shortlisted property";

        public const string PromoteId = "27a458a5-4b64-4f3f-ad9f-69185de125f8";
        public const string PromoteRouteId = "SP_Promote";
        public const string PromoteDescription = "Promote a shortlisted property to a live investment";
    }
}