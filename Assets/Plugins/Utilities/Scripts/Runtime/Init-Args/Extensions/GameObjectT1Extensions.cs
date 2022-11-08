using System;
using UnityEngine;


namespace Utils.Init
{
    public static class GameObjectT1Extensions
    {
        public static TComponent Init<TComponent>(this GameObject<TComponent> @this) where TComponent : Component
        {
            if (typeof(TComponent) is not IArgs)
            {
                return @this;
            }
            else
            {
                throw new Exception($"{typeof(TComponent)} does not init with 0 Arguments.");
            }
        }

        public static TComponent Init<TComponent, TArgument>(this GameObject<TComponent> @this, TArgument argument)
                            where TComponent : Component, IInitable<TArgument>, IOneArgument
        {
            ((TComponent)@this).Init(argument);
            return @this;
        }

        public static TComponent Init<TComponent, TFirstArgument, TSecondArgument>
            (this GameObject<TComponent> @this, TFirstArgument firstArgument, TSecondArgument secondArgument)
            where TComponent : Component, IInitable<TFirstArgument, TSecondArgument>, ITwoArguments
        {
            ((TComponent)@this).Init(firstArgument, secondArgument);
            return @this;
        }

        public static TComponent Init<TComponent, TFirstArgument, TSecondArgument, TThirdArgument>
            (this GameObject<TComponent> @this, TFirstArgument firstArgument, TSecondArgument secondArgument, TThirdArgument thirdArgument)
            where TComponent : Component, IInitable<TFirstArgument, TSecondArgument, TThirdArgument>, IThreeArguments
        {
            ((TComponent)@this).Init(firstArgument, secondArgument, thirdArgument);
            return @this;
        }

        public static TComponent Init<TComponent, TFirstArgument, TSecondArgument, TThirdArgument, TFourthArgument>
            (this GameObject<TComponent> @this, TFirstArgument firstArgument, TSecondArgument secondArgument, TThirdArgument thirdArgument, TFourthArgument fourthArgument)
            where TComponent : Component, IInitable<TFirstArgument, TSecondArgument, TThirdArgument, TFourthArgument>, IFourArguments
        {
            ((TComponent)@this).Init(firstArgument, secondArgument, thirdArgument, fourthArgument);
            return @this;
        }

        public static TComponent Init<TComponent, TFirstArgument, TSecondArgument, TThirdArgument, TFourthArgument, TFifthArgument>
            (this GameObject<TComponent> @this, TFirstArgument firstArgument, TSecondArgument secondArgument, TThirdArgument thirdArgument, TFourthArgument fourthArgument, TFifthArgument fifthArgument)
            where TComponent : Component, IInitable<TFirstArgument, TSecondArgument, TThirdArgument, TFourthArgument, TFifthArgument>, IFiveArguments
        {
            ((TComponent)@this).Init(firstArgument, secondArgument, thirdArgument, fourthArgument, fifthArgument);
            return @this;
        }
    }
}