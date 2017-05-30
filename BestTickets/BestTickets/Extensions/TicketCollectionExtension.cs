﻿using System.Collections.Generic;
using System.Linq;
using BestTickets.Models;
using System;

namespace BestTickets.Extensions
{
    public static class TicketCollectionExtension
    {
        public static double GetAverageTicketsPrice(this IEnumerable<Vehicle> tickets)
        {
            var ticketPrices = tickets.SelectMany(x => x.Places.Select(y => y.Cost));
            double averageTicketPrice = 0;
            if(ticketPrices.Count() != 0)
                averageTicketPrice = (double)ticketPrices.Average();
            return averageTicketPrice;
        }

        public static IEnumerable<Vehicle> GroupTicketsByAveragePrice(this IEnumerable<Vehicle> tickets, double averagePrice)
        {
            foreach (var ticket in tickets)
            {
                var places = ticket.Places.ToList();
                foreach (var place in places)
                {
                    if (place.Cost <= averagePrice)
                        place.isCostLessThanAverage = true;
                }
                ticket.Places = places;
                yield return ticket;
            }
        }

        public static IEnumerable<Vehicle> GetTicketsByPrice(this IEnumerable<Vehicle> tickets, double? price)
        {
            //Not work yet
            IEnumerable<Vehicle> filteredTickets = null;
            if (price == null)
                filteredTickets = tickets;
            else
                filteredTickets = tickets.Where(x => x.Places.Min().Cost <= price);
            return filteredTickets;
        }

        public static IEnumerable<Vehicle> GetTicketsByTimeOrNearest(this IEnumerable<Vehicle> tickets, TimeSpan? time)
        {
            var hours = time.Value.Hours;
            var result = tickets.Where(x => x.DepartureTime.Take(2).ToString().Equals(hours.ToString()));
            if (result.Count() == 0)
                result = tickets.OrderBy(x => x.DepartureTime).Where(x => int.Parse(x.DepartureTime.Substring(0, 2)) >= (hours)).Take(3);
            return result;
        }

    }
}
