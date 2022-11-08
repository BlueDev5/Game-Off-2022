using System;
using UnityEngine;


namespace Utils.Init
{
    public static class GameObjectT2Extensions
    {
        public static UninitializedGameObject<TFirstComponent, TSecondComponent> Init1<TFirstComponent, TSecondComponent>
            (this GameObject<TFirstComponent, TSecondComponent> @this)
            where TFirstComponent : Component where TSecondComponent : Component
        {
            if (typeof(TFirstComponent) is not IArgs)
            {
                @this.GetGameObjectData().AddComponent<TFirstComponent>();
                return new UninitializedGameObject<TFirstComponent, TSecondComponent>(@this.GetGameObjectData());
            }

            throw new Exception($"{typeof(TFirstComponent)} does not init with 0 arguments.");
        }

        public static UninitializedGameObject<TFirstComponent, TSecondComponent> Init1
            <TFirstComponent, TSecondComponent, TArgument>
            (this GameObject<TFirstComponent, TSecondComponent> @this, TArgument argument)
            where TFirstComponent : MonoBehaviour, IOneArgument, IInitable<TArgument> where TSecondComponent : Component
        {
            @this.GetGameObjectData().AddComponent<TFirstComponent>();
            ((TFirstComponent)@this).Init(argument);
            return new UninitializedGameObject<TFirstComponent, TSecondComponent>(@this.GetGameObjectData());
        }

        public static UninitializedGameObject<TFirstComponent, TSecondComponent> Init1
            <TFirstComponent, TSecondComponent, TFirstArgument, TSecondArgument>
            (this GameObject<TFirstComponent, TSecondComponent> @this, TFirstArgument firstArgument, TSecondArgument secondArgument)
            where TFirstComponent : MonoBehaviour, ITwoArguments, IInitable<TFirstArgument, TSecondArgument> where TSecondComponent : Component
        {
            @this.GetGameObjectData().AddComponent<TFirstComponent>();
            ((TFirstComponent)@this).Init(firstArgument, secondArgument);
            return new UninitializedGameObject<TFirstComponent, TSecondComponent>(@this.GetGameObjectData());
        }

        public static UninitializedGameObject<TFirstComponent, TSecondComponent> Init1
            <TFirstComponent, TSecondComponent, TFirstArgument, TSecondArgument, TThirdArgument>
            (this GameObject<TFirstComponent, TSecondComponent> @this, TFirstArgument firstArgument, TSecondArgument secondArgument, TThirdArgument thirdArgument)
            where TFirstComponent : MonoBehaviour, IThreeArguments, IInitable<TFirstArgument, TSecondArgument, TThirdArgument> where TSecondComponent : Component
        {
            @this.GetGameObjectData().AddComponent<TFirstComponent>();
            ((TFirstComponent)@this).Init(firstArgument, secondArgument, thirdArgument);
            return new UninitializedGameObject<TFirstComponent, TSecondComponent>(@this.GetGameObjectData());
        }

        public static UninitializedGameObject<TFirstComponent, TSecondComponent> Init1
            <TFirstComponent, TSecondComponent, TFirstArgument, TSecondArgument, TThirdArgument, TFourthArgument>
            (this GameObject<TFirstComponent, TSecondComponent> @this, TFirstArgument firstArgument, TSecondArgument secondArgument, TThirdArgument thirdArgument, TFourthArgument fourthArgument)
            where TFirstComponent : MonoBehaviour, IFourArguments, IInitable<TFirstArgument, TSecondArgument, TThirdArgument, TFourthArgument> where TSecondComponent : Component
        {
            @this.GetGameObjectData().AddComponent<TFirstComponent>();
            ((TFirstComponent)@this).Init(firstArgument, secondArgument, thirdArgument, fourthArgument);
            return new UninitializedGameObject<TFirstComponent, TSecondComponent>(@this.GetGameObjectData());
        }

        public static UninitializedGameObject<TFirstComponent, TSecondComponent> Init1
            <TFirstComponent, TSecondComponent, TFirstArgument, TSecondArgument, TThirdArgument, TFourthArgument, TFifthArgument>
            (this GameObject<TFirstComponent, TSecondComponent> @this, TFirstArgument firstArgument, TSecondArgument secondArgument, TThirdArgument thirdArgument, TFourthArgument fourthArgument, TFifthArgument fifthArgument)
            where TFirstComponent : MonoBehaviour, IFiveArguments, IInitable<TFirstArgument, TSecondArgument, TThirdArgument, TFourthArgument, TFifthArgument> where TSecondComponent : Component
        {
            @this.GetGameObjectData().AddComponent<TFirstComponent>();
            ((TFirstComponent)@this).Init(firstArgument, secondArgument, thirdArgument, fourthArgument, fifthArgument);
            return new UninitializedGameObject<TFirstComponent, TSecondComponent>(@this.GetGameObjectData());
        }


        public static Components<TFirstComponent, TSecondComponent> Init2<TFirstComponent, TSecondComponent>
            (this UninitializedGameObject<TFirstComponent, TSecondComponent> @this)
            where TFirstComponent : Component where TSecondComponent : Component
        {
            if (typeof(TFirstComponent) is not IArgs)
            {
                @this.GetGameObjectData().AddComponent<TSecondComponent>();
                return new Components<TFirstComponent, TSecondComponent>(@this, @this);
            }

            throw new Exception($"{typeof(TFirstComponent)} does not init with 0 arguments.");
        }

        public static Components<TFirstComponent, TSecondComponent> Init2
            <TFirstComponent, TSecondComponent, TArgument>
            (this GameObject<TFirstComponent, TSecondComponent> @this, TArgument argument)
            where TSecondComponent : MonoBehaviour, IOneArgument, IInitable<TArgument> where TFirstComponent : Component
        {
            @this.GetGameObjectData().AddComponent<TSecondComponent>();
            ((TSecondComponent)@this).Init(argument);
            return new Components<TFirstComponent, TSecondComponent>(@this, @this);
        }

        public static Components<TFirstComponent, TSecondComponent> Init2
            <TFirstComponent, TSecondComponent, TFirstArgument, TSecondArgument>
            (this UninitializedGameObject<TFirstComponent, TSecondComponent> @this, TFirstArgument firstArgument, TSecondArgument secondArgument)
            where TSecondComponent : MonoBehaviour, ITwoArguments, IInitable<TFirstArgument, TSecondArgument> where TFirstComponent : Component
        {
            @this.GetGameObjectData().AddComponent<TSecondComponent>();
            ((TSecondComponent)@this).Init(firstArgument, secondArgument);
            return new Components<TFirstComponent, TSecondComponent>(@this, @this);
        }

        public static Components<TFirstComponent, TSecondComponent> Init2
            <TFirstComponent, TSecondComponent, TFirstArgument, TSecondArgument, TThirdArgument>
            (this UninitializedGameObject<TFirstComponent, TSecondComponent> @this, TFirstArgument firstArgument, TSecondArgument secondArgument, TThirdArgument thirdArgument)
            where TSecondComponent : MonoBehaviour, IThreeArguments, IInitable<TFirstArgument, TSecondArgument, TThirdArgument> where TFirstComponent : Component
        {
            @this.GetGameObjectData().AddComponent<TSecondComponent>();
            ((TSecondComponent)@this).Init(firstArgument, secondArgument, thirdArgument);
            return new Components<TFirstComponent, TSecondComponent>(@this, @this);
        }

        public static Components<TFirstComponent, TSecondComponent> Init2
            <TFirstComponent, TSecondComponent, TFirstArgument, TSecondArgument, TThirdArgument, TFourthArgument>
            (this UninitializedGameObject<TFirstComponent, TSecondComponent> @this, TFirstArgument firstArgument, TSecondArgument secondArgument, TThirdArgument thirdArgument, TFourthArgument fourthArgument)
            where TSecondComponent : MonoBehaviour, IFourArguments, IInitable<TFirstArgument, TSecondArgument, TThirdArgument, TFourthArgument> where TFirstComponent : Component
        {
            @this.GetGameObjectData().AddComponent<TSecondComponent>();
            ((TSecondComponent)@this).Init(firstArgument, secondArgument, thirdArgument, fourthArgument);
            return new Components<TFirstComponent, TSecondComponent>(@this, @this);
        }

        public static Components<TFirstComponent, TSecondComponent> Init2
            <TFirstComponent, TSecondComponent, TFirstArgument, TSecondArgument, TThirdArgument, TFourthArgument, TFifthArgument>
            (this UninitializedGameObject<TFirstComponent, TSecondComponent> @this, TFirstArgument firstArgument, TSecondArgument secondArgument, TThirdArgument thirdArgument, TFourthArgument fourthArgument, TFifthArgument fifthArgument)
            where TSecondComponent : MonoBehaviour, IFiveArguments, IInitable<TFirstArgument, TSecondArgument, TThirdArgument, TFourthArgument, TFifthArgument> where TFirstComponent : Component
        {
            @this.GetGameObjectData().AddComponent<TSecondComponent>();
            ((TSecondComponent)@this).Init(firstArgument, secondArgument, thirdArgument, fourthArgument, fifthArgument);
            return new Components<TFirstComponent, TSecondComponent>(@this, @this);
        }
    }
}