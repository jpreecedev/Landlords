namespace Model
{
    using System;
    using Model.Database;
    using Validation;

    public class ApplicationUserPortfolio : BaseModel, IUserEntity
    {
        [ValidateGuid]
        public Guid PortfolioId { get; set; }

        public Portfolio Portfolio { get; set; }

        [ValidateGuid]
        public Guid UserId { get; set; }

        public ApplicationUser User { get; set; }

        [ValidateGuid]
        public Guid? AgencyId { get; set; }

        public Agency Agency { get; set; }
    }
}