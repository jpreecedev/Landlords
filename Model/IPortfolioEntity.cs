namespace Model
{
    using System;

    public interface IPortfolioEntity
    {
        Guid PortfolioId { get; set; }

        Portfolio Portfolio { get; set; }
    }
}