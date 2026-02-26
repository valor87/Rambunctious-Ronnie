using NodeCanvas.Framework;
using UnityEngine;
using UnityEngine.AI;


namespace NodeCanvas.Tasks.Actions {

	public class WalkAroundAT : ActionTask {
		public float randomPositionDistance;
		public float arrivalDistance;

		private NavMeshAgent navMeshAgent;
		private Vector3 destination;

		//Use for initialization. This is called only once in the lifetime of the task.
		//Return null if init was successfull. Return an error string otherwise
		protected override string OnInit() {
			navMeshAgent = agent.gameObject.GetComponent<NavMeshAgent>();
			return null;
		}

		//This is called once each time the task is enabled.
		//Call EndAction() to mark the action as finished, either in success or failure.
		//EndAction can be called from anywhere.
		protected override void OnExecute() {
			FindAPath();
		}
		void FindAPath()
		{
			Vector3 randomPosition = randomPositionDistance * Random.insideUnitSphere + agent.transform.position;
			NavMeshHit navHit = new NavMeshHit();

			if (!NavMesh.SamplePosition(randomPosition, out navHit, randomPositionDistance, NavMesh.AllAreas))
			{
				Debug.Log("Did not find a path");
				
				EndAction();
			}
			else
			{
				destination = navHit.position;

				navMeshAgent.SetDestination(destination);
			}
		}
		//Called once per frame while the action is active.
		protected override void OnUpdate() {
			Debug.Log(destination);
			float distance = Vector3.Distance(agent.transform.position, destination);
			if (navMeshAgent.pathStatus == NavMeshPathStatus.PathComplete && distance < arrivalDistance)
			{
				Debug.Log("Done pathing");
				EndAction(true);
			}
		}

		//Called when the task is disabled.
		protected override void OnStop() {
			
		}

		//Called when the task is paused.
		protected override void OnPause() {
			
		}
	}
}