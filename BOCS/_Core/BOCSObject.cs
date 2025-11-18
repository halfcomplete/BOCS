using System;
using System.Collections.Generic;
using System.Linq;

namespace BOCS._Core
{
    /// <summary>
    /// A base class for all objects in the BOCS system, with support for adding and managing behaviours.
    /// </summary>
    public class BOCSObject
    {
        /// <summary>
        /// The name of the object, used for display.
        /// </summary>
        public string Name { get; }

        /// <summary>
        /// A unique identifier for the object.
        /// </summary>
        public string ID { get; } = Guid.NewGuid().ToString();

        /// <summary>
        /// A dictionary mapping interface types to lists of Behaviours that implement those interface types.
        /// </summary>
        public Dictionary<IAction, List<IBehaviour>> Behaviours { get; private set; } = new Dictionary<IAction, List<IBehaviour>>();
        
        public BOCSObject(string name)
        {
            Name = name;
        }

        #region Behaviours
        /// <summary>
        /// Adds a Behaviour to this object.
        /// </summary>
        /// <param name="behaviour">The Behaviour to add, of the type BehaviourBase.</param>
        /// <remarks>
        /// This works by checking all the Actions that the Behaviour implements, and adding it to the list of Behaviours for each Action.
        /// </remarks>
        public void AddBehaviour(BehaviourBase behaviour)
        {
            if (behaviour == null)
                throw new ArgumentNullException();

            // Get all the Behaviour Interfaces that the Behaviour implements
            var actions = behaviour.GetType().GetInterfaces().OfType<IAction>();
            

            // Loop over all the Behaviour Interfaces that the Behaviour implements
            foreach (IAction action in actions)
            {
                // Initialise the list if it doesn't exist
                if (!Behaviours.ContainsKey(action))
                {
                    Behaviours[action] = new List<IBehaviour>();
                }

                // Add the Behavior to the list
                Behaviours[action].Add(behaviour);
            }
        }

        /// <summary>
        /// Remove all Behaviours in this object that implement the given Action.
        /// </summary>
        /// <typeparam name="Action">The Action that must be implemented by a Behaviour in Behaviours to be removed by this method.</typeparam>
        /// <returns></returns>
        public int RemoveAllBehavioursOfType<Action>() where Action : IAction
        {
            List<BehaviourBase> targetBehaviours = Behaviours[typeof(Action)];

            // Remove all instances of the targetBehaviours from Behaviours
            Behaviours.Where(kvp =>
            {
                // Remove all the behaviours in the list where they are part of the targetBehaviours
                kvp.Value.RemoveAll(behaviour => targetBehaviours.Contains(behaviour));
                // Return true if not all Behaviours were removed from the list
                // Thus, remove all key-value pairs from Behaviours that had all their interfaces removed
                return kvp.Value.Count() != 0;
            });

            // Return how many unique targetBehaviours were removed
            return targetBehaviours.Count();
        }

        /// <typeparam name="Action">The Behaviour to check for presence in this object.</typeparam>
        /// <returns>A bool representing whether this object contains the Behaviour specified.</returns> 
        public bool HasBehaviourOfType<Action>() where Action : IAction
        {
            return Behaviours.Values.Any(l => l.Any(b => b.GetType().Equals(typeof(Action))));
        }

        /// <typeparam name="Action"></typeparam>
        /// <returns>An IEnumerable containing all Behaviours that implement the provided Action.</returns>
        public IEnumerable<Action> GetAllBehavioursOfType<Action>() where Action : IAction
        {
            return Behaviours[typeof(Action)].Cast<Action>();
        }

        #endregion Behaviours
    }
}