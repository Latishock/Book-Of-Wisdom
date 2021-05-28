using System;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;
using UnityEngine;

namespace BookOfWisdom
{
    

    namespace Patterns
    {
        public static class Singleton
        {
            /// <summary>
            /// Checks if only one instance of Type T exists.
            /// </summary>
            /// <typeparam name="T"></typeparam>
            /// <param name="instance">Type that you want to check (use keyword "this").</param>
            /// <param name="instanceField">Reference to instance field in Type.</param>
            /// <param name="instanceGameObject">Type gameObject.</param>
            public static void Initialize<T>(T instance, ref T instanceField, GameObject instanceGameObject)
            {
                if (instanceField == null)
                {
                    instanceField = instance;
                }
                else
                {
                    UnityEngine.Object.Destroy(instanceGameObject);
                }
            }
        }
        public class StateMachine<T> where T : class
        {
            public State<T> CurrentState { get; private set; }

            public void Initialize(State<T> startingState)
            {
                CurrentState = startingState;
                startingState.Enter();
            }

            public void ChangeState(State<T> newState)
            {
                CurrentState.Exit();

                CurrentState = newState;
                newState.Enter();
            }
        }
        public abstract class State<T> where T : class
        {
            protected T StateObject;
            protected StateMachine<T> StateMachine;

            protected State(T stateObject, StateMachine<T> stateMachine)
            {
                this.StateObject = stateObject;
                this.StateMachine = stateMachine;
            }

            public virtual void Enter()
            {

            }

            public virtual void HandleInput()
            {

            }

            public virtual void LogicalUpdate()
            {

            }

            public virtual void PhysicsUpdate()
            {

            }
            public virtual void Exit()
            {

            }

        }
    }

    namespace SharedVariables
    {
        public class SharedVariable
        {
            public static List<SharedVariable> SharedVariables = new List<SharedVariable>();
            public string Name;
            public dynamic Value;
            public Type Type;
            public SharedVariable() { }
            public SharedVariable(string name, dynamic value)
            {
                Name = name;
                Value = value;
                Type = GetVariableType(Value);
                
                foreach (SharedVariable variable in SharedVariables)
                {
                    if (name == variable.Name)
                    {
                        SharedVariable sameVariable = SharedVariables.Single(x => x.Name == name);
                        SharedVariables.Remove(sameVariable);
                    }
                }
                SharedVariables.Add(this);
            }
            public static Type GetVariableType<T>(T t) { return typeof(T); }


            public static string SerializeToJson()
            {
                return JsonConvert.SerializeObject(SharedVariables);
            }

            public static void DeserializeFromJson(ref List<SharedVariable> container, string json)
            {
                container = JsonConvert.DeserializeObject<List<SharedVariable>>(json);
            }

            public static dynamic Cast( dynamic obj, Type castTo)
            {
                return Convert.ChangeType(obj, castTo);
            }

            public static dynamic GetValue(string variableName)
            {
                foreach (SharedVariable variable in SharedVariable.SharedVariables)
                {
                    if (variableName == variable.Name)
                    {
                        return Cast(variable.Value, variable.Type);
                    }
                }
                throw new Exception("There is no variable with that name in SharedVariables List");
            }

            public string GetStringValue()
            {
                return Value.ToString();
            }
        }
    }
}