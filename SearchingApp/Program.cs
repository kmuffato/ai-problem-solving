using ProblemSolving.BySearching.Strategy;
using PuzzleEnvironment;
using SearchingApp.Problems.EightPuzzle;
using System;
using System.Linq;

namespace SearchingApp
{
    class Program
    {
        static void Main(string[] args)
        {
            var environment = new PuzzleEnvironment<int>(GetEightPuzzleBoard());

            var bfs = new BreadthFirstStrategy<ProblemState<int>, ProblemAction>();

            var ucs = new UniformCostStrategy<ProblemState<int>, ProblemAction>();

            var ucs2 = new UniformCostStrategy2<ProblemState<int>, ProblemAction>();

            var dfs = new DepthFirstStrategy<ProblemState<int>, ProblemAction>();

            var dls = new DepthLimitedStrategy<ProblemState<int>, ProblemAction>(10);

            var ids = new IterativeDeepeningStrategy<ProblemState<int>, ProblemAction>();

            var greedy = new GreedyBestFirstStrategy<ProblemState<int>, ProblemAction>(
                HeuristicFunctions.StraightLineDistance);

            var astar = new AStarStrategy<ProblemState<int>, ProblemAction>(
                HeuristicFunctions.StraightLineDistance);

            var agent = new SearchAgent<int>(astar);

            RunAgent(environment, agent);
        }

        public static PuzzleBoard<int> GetEightPuzzleBoard()
        {
            return new[] { 1, 2, 3, default, 4, 5, 6, 7, 8 }
                .OrderBy(x => new Random().Next())
                .ToArray()
                .ToPuzzleBoard(3, 3);
        }

        private static void RunAgent(
            PuzzleEnvironment<int> environment, SearchAgent<int> agent)
        {
            Console.WriteLine($"Agent starting with {agent.SearchStrategy.GetType().Name}");

            Console.WriteLine($"Value represents empty for {nameof(Int32)}: {default(int)}");

            Console.WriteLine("Environment Initial State");

            WriteState(environment.State);

            ProblemAction action;

            int agentSteps = 0;

            while ((action = agent.Work(environment.State)) != default)
            {
                var newState = agent.Problem
                    .TransitionModel(environment.State.ToProblemState(), action)
                    .ToPuzzleBoard();

                environment = new PuzzleEnvironment<int>(newState);

                Console.Write($"{action.Move} -> ");

                agentSteps++;
            }

            Console.WriteLine();
            Console.WriteLine("Search Agent is {0} changed Environment state", agentSteps != 0 ? $"{agentSteps} times" : "not");

            WriteState(environment.State);

            var validation = agent.Problem.GoalTest(environment.State.ToProblemState());

            Console.WriteLine($"Is agent goal validated: {validation}");
        }

        public static void WriteState(PuzzleBoard<int> state)
        {
            for (int i = 0; i < state.Pieces.GetLength(0); i++)
            {
                Console.Write("[");
                for (int j = 0; j < state.Pieces.GetLength(1); j++)
                {
                    Console.Write($"{state.Pieces[i, j].Value,3}");
                }
                Console.WriteLine("]");
            }
        }
    }
}
