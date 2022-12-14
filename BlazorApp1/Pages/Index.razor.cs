using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;
using System.Net.Http;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.Forms;
using Microsoft.AspNetCore.Components.Routing;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.Web.Virtualization;
using Microsoft.JSInterop;
using BlazorApp1;
using BlazorApp1.Shared;
using BlazingPizza;
using BlazorApp1.Services.Interfaces;
using BlazorApp1.Services.Implements;

namespace BlazorApp1.Pages
{

    public partial class Index
    {
        [Inject]
        public IPizzaService PizzaService { get; set; }

        [Inject]
        public OrderState OrderState { set; get; }


        private IEnumerable<PizzaSpecial> todaysPizzas;
        private Order order => OrderState.Order;


        protected override async Task OnInitializedAsync()
        {
            todaysPizzas = await PizzaService.GetPizzasAsync();
        }
    }
}