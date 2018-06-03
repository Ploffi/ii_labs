namespace PerceptronApp
{
	class LogicFunc
	{
		private readonly BoolNeuron _neuron;
		private const double LearningRate = 0.1;

		public LogicFunc()
		{
			_neuron = new BoolNeuron(2, (sum) => sum > 0 ? 1 : 0);
		}

		public void Train(double[] input, double expected)
		{
			var calcResult = _neuron.Calculate(input);
			var error = expected - calcResult;

			_neuron.UpdateWeights(input, error, LearningRate);
		}

		public double Calculate(double[] input)
		{
			return _neuron.Calculate(input);
		}
	}
}
