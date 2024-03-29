﻿using AdaptiveCards;
using BestTickets.Extensions;
using BestTickets.Services;
using RouteHelpBot.Extensions;
using RouteHelpBot.Model;


namespace RouteHelpBot.BLL
{
    public class ResponseFactory
    {

        public static AdaptiveCard CreateResponseAsAdaptiveCard(UserRequest request)
        {
            AdaptiveCard card = new AdaptiveCard();
            if (request == null)
                card = AdaptiveCardFeedbackGenerator.GenerateTextCard(TextFeedbackGenerator.MakeWrongRouteFeedbackUntrivial());
            else if (string.IsNullOrEmpty(request.Route.ArrivalPlace) || string.IsNullOrEmpty(request.Route.DeparturePlace))
            {
                if (request.KeyWord == "Приветствие")
                    card = AdaptiveCardFeedbackGenerator.GenerateTextCard(TextFeedbackGenerator.MakeGreetingFeedbackUntrivial());
                else
                    card = AdaptiveCardFeedbackGenerator.GenerateTextCard(TextFeedbackGenerator.MakeWrongRouteFeedbackUntrivial());
            }
            else
            {
                var tickets = new TicketsFactory().GetTicketFinder(request.VehicleKind).SearchTickets(request.Route)
                                                .GetTicketsByPrice(request.Price).GetTicketsByTimeOrNearest(request.Time);
                card = new  AdaptiveCardFeedbackGenerator().GenerateFeedback(tickets);
            }

            return card;
        }

        public static string CreateResponseAsText(UserRequest request)
        {
            string responseText;
            if(request == null)
                responseText = TextFeedbackGenerator.MakeWrongRouteFeedbackUntrivial();
            else if (string.IsNullOrEmpty(request.Route.ArrivalPlace) || string.IsNullOrEmpty(request.Route.DeparturePlace))
            {
                if (request.KeyWord == "Приветствие")
                    responseText = TextFeedbackGenerator.MakeGreetingFeedbackUntrivial();
                else
                    responseText = TextFeedbackGenerator.MakeWrongRouteFeedbackUntrivial();
            }
            else
            {
                var tickets = new TicketsFactory().GetTicketFinder(request.VehicleKind).SearchTickets(request.Route)
                                                  .GetTicketsByPrice(request.Price).GetTicketsByTimeOrNearest(request.Time);
                responseText = new TextFeedbackGenerator().GenerateFeedback(tickets);
            }
            return responseText;
        }


    }
}