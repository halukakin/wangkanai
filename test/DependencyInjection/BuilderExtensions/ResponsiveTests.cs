// Copyright (c) 2014-2020 Sarin Na Wangkanai, All Rights Reserved.
// The Apache v2. See License.txt in the project root for license information.

using System;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Abstractions;
using Moq;
using Wangkanai.Detection.DependencyInjection.Options;
using Wangkanai.Detection.Models;
using Wangkanai.Detection.Services;
using Xunit;

namespace Wangkanai.Detection.Hosting
{
    public class ResponsiveCollectionExtensionsTests
    {
        private readonly int total = 17;

        [Fact]
        public void AddResponsive_Services()
        {
            var service  = new ServiceCollection();
            var expected = service.Count + total;
            var builder  = service.AddDetection();

            //Assert.Equal(expected, builder.Services.Count);
            Assert.Same(service, builder.Services);
        }

        [Fact]
        public void AddResponsive_Null_ArgumentNullException()
        {
            Assert.Throws<ArgumentNullException>(CreateResponsiveNullService);
        }

        [Fact]
        public void AddResponsive_Options_Builder_Service()
        {
            var service  = new ServiceCollection();
            var expected = service.Count + total;
            var builder  = service.AddDetection(options => { options.Responsive.DefaultTablet = Device.Desktop; });

            //Assert.Equal(expected, builder.Services.Count);
            Assert.Same(service, builder.Services);
        }

        [Fact]
        public void AddResponsive_Options_Null_ArgumentNullException()
        {
            Assert.Throws<ArgumentNullException>(CreateResponsiveNullService);
        }

        private readonly Func<object> CreateResponsiveNullService =
            () => ((IServiceCollection) null).AddDetection();

        [Fact]
        public async void AddResponsive_Options_Disable_True()
        {
            var service  = MockService.CreateService("mobile");
            var options  = new DetectionOptions {Responsive = {Disable = true}};
            var resolver = MockResponsiveService(service, options);

            var app = MockApplicationBuilder(options, resolver);

            app.UseDetection();

            var request = app.Build();

            Assert.NotEqual(service.Context, new DefaultHttpContext());

            await request.Invoke(service.Context);

            //Assert.Equal(Device.Mobile, service.Context.GetDevice());
        }

        [Fact]
        public async void AddResponsive_Options_Disable_False()
        {
            var service  = MockService.CreateService("mobile");
            var options  = new DetectionOptions {Responsive = {Disable = false}};
            var resolver = MockResponsiveService(service, options);

            var app = MockApplicationBuilder(options, resolver);

            app.UseDetection();

            var request = app.Build();

            Assert.NotEqual(service.Context, new DefaultHttpContext());

            await request.Invoke(service.Context);

            Assert.Equal(Device.Mobile, service.Context.GetDevice());
        }

        [Theory]
        [InlineData(false, Device.Desktop, "mobile", "")]
        [InlineData(false, Device.Desktop, "mobile", "/api/dog")]
        [InlineData(false, Device.Desktop, "desktop", "")]
        [InlineData(false, Device.Desktop, "desktop", "/api/dog")]
        [InlineData(false, Device.Mobile, "desktop", "")]
        [InlineData(false, Device.Mobile, "desktop", "/api/dog")]
        [InlineData(false, Device.Mobile, "mobile", "")]
        [InlineData(false, Device.Mobile, "mobile", "/api/dog")]
        [InlineData(true, Device.Desktop, "mobile", "")]
        [InlineData(true, Device.Desktop, "mobile", "/api/dog")]
        [InlineData(true, Device.Desktop, "desktop", "")]
        [InlineData(true, Device.Desktop, "desktop", "/api/dog")]
        [InlineData(true, Device.Mobile, "desktop", "")]
        [InlineData(true, Device.Mobile, "desktop", "/api/dog")]
        [InlineData(true, Device.Mobile, "mobile", "")]
        [InlineData(true, Device.Mobile, "mobile", "/api/dog")]
        public async void AddResponsive_WebApi(bool include, Device device, string agent, string path)
        {
            var service = MockService.CreateService(agent);
            service.Context.Request.Path = path;
            var options = new DetectionOptions
            {
                Responsive =
                {
                    DefaultMobile  = device,
                    DefaultTablet  = device,
                    DefaultDesktop = device,
                    IncludeWebApi  = include
                }
            };
            var resolver = MockResponsiveService(service, options);

            var app = MockApplicationBuilder(options, resolver);

            app.UseDetection();

            var requestDelegate = app.Build();

            Assert.Equal(path, service.Context.Request.Path);
            await requestDelegate.Invoke(service.Context);
            //Assert.Throws<ArgumentNullException>(()=>service.Context.GetDevice());
            Assert.Equal(device, service.Context.GetDevice());
        }

        private static ResponsiveService MockResponsiveService(IUserAgentService service, DetectionOptions options)
        {
            var device     = new DeviceService(service);
            var preference = new PreferenceService();
            var resolver   = new ResponsiveService(device, preference, options);
            return resolver;
        }

        private static ApplicationBuilder MockApplicationBuilder(DetectionOptions options, ResponsiveService resolver)
        {
            var serviceProvider = new Mock<IServiceProvider>();

            serviceProvider
                .Setup(s => s.GetService(typeof(ILoggerFactory)))
                .Returns(Mock.Of<NullLoggerFactory>());
            serviceProvider
                .Setup(s => s.GetService(typeof(DetectionOptions)))
                .Returns(options);
            serviceProvider
                .Setup(s => s.GetService(typeof(IResponsiveService)))
                .Returns(resolver);
            serviceProvider
                .Setup(s => s.GetService(typeof(MarkerService)))
                .Returns(new MarkerService());

            return new ApplicationBuilder(serviceProvider.Object);
        }
    }
}