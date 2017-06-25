namespace Model.Entities
{
    using System;
    using Model.Database;
    using Validation;

    public class ApplicationUserPortfolio : BaseModel, IUserEntity
    {
        [RequiredGuid]
        public Guid PortfolioId { get; set; }

        public Portfolio Portfolio { get; set; }

        [RequiredGuid]
        public Guid UserId { get; set; }

        public ApplicationUser User { get; set; }

        [RequiredGuid]
        public Guid? AgencyId { get; set; }

        public Agency Agency { get; set; }
    }
}