using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Models;
using api.Models.DTOs.Stock;

namespace api.Mappers
{
    public static class StockMappers
    {
        public static StockDto ToStockDto(this Stock stockmodel)
        {
            return new StockDto
            {
                Id = stockmodel.Id,
                Symbol = stockmodel.Symbol,
                CompanyName = stockmodel.CompanyName,
                Industry = stockmodel.Industry,
                LastDiv = stockmodel.LastDiv,
                MarketCap = stockmodel.MarketCap,
                Purchase = stockmodel.Purchase,

            };
        }

        public static Stock ToStockFromCreateDTO(this CreateStockRequestDto stockDto)
        {
            return new Stock
            {
                Symbol = stockDto.Symbol,
                CompanyName = stockDto.CompanyName,
                Industry = stockDto.Industry,
                LastDiv = stockDto.LastDiv,
                MarketCap = stockDto.MarketCap,
                Purchase = stockDto.Purchase,
            };
        }
    }
}