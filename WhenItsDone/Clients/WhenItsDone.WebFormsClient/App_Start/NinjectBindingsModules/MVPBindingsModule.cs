﻿using System;
using System.Linq;

using Ninject;
using Ninject.Activation;
using Ninject.Extensions.Conventions;
using Ninject.Extensions.Factory;
using Ninject.Modules;
using Ninject.Parameters;

using WebFormsMvp;
using WebFormsMvp.Binder;

using WhenItsDone.MVP.AssemblyId;
using WhenItsDone.WebFormsClient.App_Start.PresenterFactories;

namespace WhenItsDone.WebFormsClient.App_Start.NinjectBindingsModules
{
    public class MVPBindingsModule : NinjectModule
    {
        public override void Load()
        {
            this.Kernel.Bind(x =>
                x.FromAssemblyContaining<IMvpAssemblyId>()
                .SelectAllClasses()
                .BindDefaultInterface()
            );

            this.Bind<IPresenterFactory>()
                .To<WebFormsMvpPresenterFactory>()
                .InSingletonScope();

            this.Bind<ICustomPresenterFactory>()
                .ToFactory()
                .InSingletonScope();

            this.Bind<IPresenter>()
                .ToMethod(this.PresenterFactoryMethod)
                .NamedLikeFactoryMethod((ICustomPresenterFactory factory) => factory.GetPresenter(null, null));
        }


        // Alternative binding.
        // http://webformsmvpcontrib.codeplex.com/SourceControl/latest#WebFormsMvp.Contrib/WebFormsMvp.Contrib.Ninject/MvpPresenterKernel.cs
        // Depends on correct constructor parameter name.
        private IPresenter PresenterFactoryMethod(IContext ctx)
        {
            var parameters = ctx.Parameters.ToList();

            var requestedType = (Type)parameters[0].GetValue(ctx, null);
            var viewInstance = (IView)parameters[1].GetValue(ctx, null);

            var viewInstanceParameter = new ConstructorArgument("view", viewInstance);

            return (IPresenter)ctx.Kernel.Get(requestedType, viewInstanceParameter);
        }
    }
}