using System;
using TylersPizzaChain.Database.Entities;
using TylersPizzaChain.Exceptions;

namespace TylersPizzaChain.Pipelines.BuildingBlocks.CompositionHelpers
{
	public static class CompositionHelpers
	{



        // Input: T -> TResult1, TResult1 -> TResult2.

        // Output: T -> TResult2

        //public static Func<T, TResult2> Then<T, TResult1, TResult2>(
        //    this Func<T, TResult1> first, Func<TResult1, TResult2> second) =>
        //    value => second(first(value));


        // Input, T, T -> TResult.

        // Output TResult.

        public static TResult Then<T, TResult>(this T value, Func<T, TResult> function) =>
              function(value);

        public static TResult ThenAsync<T, TResult>(this T value, Func<T, Task<TResult>> function)
        {
            var taskResult = function(value);
            taskResult.Wait();
            return taskResult.Result;
        }

        public static T GuardAgainstNull<T>(this T value, Func<Exception> newException)
        {
            if (value == null) throw newException();
            else return value;
        }

        public static bool GuardAgainstFalse(this bool value, Func<Exception> newException)
        {
            if (value == false) throw newException();
            else return value;
        }

        public static T GuardAgainstFalse<T>(this T value, Func<T, bool> prop, Func<Exception> newException)
        {
            if (prop(value) == false) throw newException();
            else return value;
        }
    }
}

