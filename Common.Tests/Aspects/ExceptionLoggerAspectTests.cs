using System;
using System.Diagnostics.CodeAnalysis;
using System.Reflection;
using Castle.DynamicProxy;
using JetBrains.Annotations;
using NSubstitute;
using NUnit.Framework;
using Ploeh.AutoFixture.NUnit3;
using Selkie.NUnit.Extensions;
using Selkie.Web.MicroServices.Common.Aspects;
using Selkie.Web.MicroServices.Common.Interfaces.Aspects;

namespace Selkie.Web.MicroServices.Common.Tests.Aspects
{
    [ExcludeFromCodeCoverage]
    [TestFixture]
    internal sealed class ExceptionLoggerAspectTests
    {
        private IInvocation CreateInvocation()
        {
            var array = new[]
                        {
                            1,
                            2
                        };

            var arguments = new object[]
                            {
                                array
                            };
            var action = new Action(DoesNotThrow);
            var invocation = Substitute.For <IInvocation>();
            invocation.TargetType.Returns(typeof( InvocationToTextConverterTests ));
            invocation.Method.Returns(action.GetMethodInfo());
            invocation.Arguments.Returns(arguments);

            return invocation;
        }

        private IInvocation CreateInvocationThatThrows()
        {
            var array = new[]
                        {
                            1,
                            2
                        };

            var arguments = new object[]
                            {
                                array
                            };
            var action = new Action(DoesThrow);
            var invocation = Substitute.For <IInvocation>();
            invocation.TargetType.Returns(typeof( InvocationToTextConverterTests ));
            invocation.Method.Returns(action.GetMethodInfo());
            invocation.Arguments.Returns(arguments);
            invocation.When(x => x.Proceed())
                      .Do(x =>
                          {
                              DoesThrow();
                          });

            return invocation;
        }

        private void DoesNotThrow()
        {
        }

        private void DoesThrow()
        {
            throw new Exception("Test");
        }

        [Theory]
        [AutoNSubstituteData]
        public void Intercept_CallsProceed_WhenCalled(
            [NotNull] ExceptionLoggerAspect sut)
        {
            // Arrange
            IInvocation invocation = CreateInvocation();

            // Act
            sut.Intercept(invocation);

            // Assert
            invocation.Received().Proceed();
        }

        [Theory]
        [AutoNSubstituteData]
        public void Intercept_DoesNotLogException_WhenProceedDoesNotThrowException(
            [NotNull, Frozen] IExceptionLogger logger,
            [NotNull] ExceptionLoggerAspect sut)
        {
            // Arrange
            IInvocation invocation = CreateInvocation();

            // Act
            sut.Intercept(invocation);

            // Assert
            logger.DidNotReceive().LogException(invocation,
                                                Arg.Any <Exception>());
        }

        [Theory]
        [AutoNSubstituteData]
        public void Intercept_LogsException_WhenProceedThrowsException(
            [NotNull, Frozen] IExceptionLogger logger,
            [NotNull] ExceptionLoggerAspect sut)
        {
            // Arrange
            IInvocation invocation = CreateInvocationThatThrows();

            // Act
            Assert.Throws <Exception>(() => sut.Intercept(invocation));

            logger.Received().LogException(invocation,
                                           Arg.Any <Exception>());
        }
    }
}