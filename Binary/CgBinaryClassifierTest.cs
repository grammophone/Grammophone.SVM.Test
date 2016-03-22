﻿using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;

using Gramma.Vectors;
using Gramma.SVM;
using Gramma.Kernels;
using Gramma.Optimization;
using Gramma.SVM.CG;

namespace SvmTest.Binary
{
	/// <summary>
	/// Summary description for UnitTest1
	/// </summary>
	[TestClass]
	public class CgBinaryClassifierTest
	{
		#region Construction

		public CgBinaryClassifierTest()
		{
			//
			// TODO: Add constructor logic here
			//
		}

		#endregion

		#region Private fields

		private TestContext testContextInstance;

		#endregion

		#region Public properties

		/// <summary>
		///Gets or sets the test context which provides
		///information about and functionality for the current test run.
		///</summary>
		public TestContext TestContext
		{
			get
			{
				return testContextInstance;
			}
			set
			{
				testContextInstance = value;
			}
		}

		#endregion

		#region Additional test attributes
		//
		// You can use the following additional attributes as you write your tests:
		//
		// Use ClassInitialize to run code before running the first test in the class
		// [ClassInitialize()]
		// public static void MyClassInitialize(TestContext testContext) { }
		//
		// Use ClassCleanup to run code after all tests in a class have run
		// [ClassCleanup()]
		// public static void MyClassCleanup() { }
		//
		// Use TestInitialize to run code before running each test 
		// [TestInitialize()]
		// public void MyTestInitialize() { }
		//
		// Use TestCleanup to run code after each test has run
		// [TestCleanup()]
		// public void MyTestCleanup() { }
		//
		#endregion

		#region Test methods

		[TestMethod]
		public void SimpleD2Test()
		{
			var trainingPairs = this.GetSimpleTrainingSetD2();

			var validationPairs = this.GetSimpleValidationSetD2();

			this.VectorTrainAndTest(
				new LinearKernel(2),
				trainingPairs,
				validationPairs,
				1.0);
		}

		[TestMethod]
		public void HorizontalD2Test()
		{
			var trainingPairs = this.GetHorizontalTrainingSetD2();

			var validationPairs = this.GetHorizontalValidationSetD2();

			this.VectorTrainAndTest(
				new LinearKernel(2),
				trainingPairs,
				validationPairs,
				100.0);
		}

		[TestMethod]
		public void SeparableD2Test()
		{
			var trainingPairs = this.GetSeparableTrainingSetD2();

			var validationPairs = this.GetSeparableValidationSetD2();

			this.VectorTrainAndTest(
				new LinearKernel(2),
				trainingPairs,
				validationPairs,
				10.0);
		}

		#endregion

		#region Provision of data points

		private IList<BinaryClassifier<Vector>.TrainingPair> GetSeparableTrainingSetD2()
		{
			var pairs = new List<BinaryClassifier<Vector>.TrainingPair>(10);

			pairs.Add(
				new BinaryClassifier<Vector>.TrainingPair() 
				{ 
					Class = BinaryClass.Negative, 
					Item = new Vector(new double[] { 0.0, 0.0 }) 
				});

			pairs.Add(
				new BinaryClassifier<Vector>.TrainingPair()
				{
					Class = BinaryClass.Negative,
					Item = new Vector(new double[] { 1.0, 0.0 })
				});

			pairs.Add(
				new BinaryClassifier<Vector>.TrainingPair()
				{
					Class = BinaryClass.Negative,
					Item = new Vector(new double[] { 0.0, 1.0 })
				});

			pairs.Add(
				new BinaryClassifier<Vector>.TrainingPair()
				{
					Class = BinaryClass.Positive,
					Item = new Vector(new double[] { 1.0, 1.0 })
				});

			pairs.Add(
				new BinaryClassifier<Vector>.TrainingPair()
				{
					Class = BinaryClass.Positive,
					Item = new Vector(new double[] { 2.0, 1.0 })
				});

			pairs.Add(
				new BinaryClassifier<Vector>.TrainingPair()
				{
					Class = BinaryClass.Positive,
					Item = new Vector(new double[] { 1.0, 2.0 })
				});

			pairs.Add(
				new BinaryClassifier<Vector>.TrainingPair()
				{
					Class = BinaryClass.Positive,
					Item = new Vector(new double[] { 2.0, 2.0 })
				});

			pairs.Add(
				new BinaryClassifier<Vector>.TrainingPair()
				{
					Class = BinaryClass.Positive,
					Item = new Vector(new double[] { 3.0, 3.0 })
				});

			pairs.Add(
				new BinaryClassifier<Vector>.TrainingPair()
				{
					Class = BinaryClass.Positive,
					Item = new Vector(new double[] { 3.0, 4.0 })
				});

			pairs.Add(
				new BinaryClassifier<Vector>.TrainingPair()
				{
					Class = BinaryClass.Negative,
					Item = new Vector(new double[] { -2.0, -3.0 })
				});

			return pairs;
		}

		private IList<BinaryClassifier<Vector>.TrainingPair> GetSeparableValidationSetD2()
		{
			var pairs = new List<BinaryClassifier<Vector>.TrainingPair>(10);

			pairs.Add(
				new BinaryClassifier<Vector>.TrainingPair()
				{
					Class = BinaryClass.Negative,
					Item = new Vector(new double[] { 0.25, 0.25 })
				});

			pairs.Add(
				new BinaryClassifier<Vector>.TrainingPair()
				{
					Class = BinaryClass.Negative,
					Item = new Vector(new double[] { -0.5, -0.5 })
				});

			pairs.Add(
				new BinaryClassifier<Vector>.TrainingPair()
				{
					Class = BinaryClass.Negative,
					Item = new Vector(new double[] { 0.0, -1.0 })
				});

			pairs.Add(
				new BinaryClassifier<Vector>.TrainingPair()
				{
					Class = BinaryClass.Positive,
					Item = new Vector(new double[] { 1.5, 1.5 })
				});

			pairs.Add(
				new BinaryClassifier<Vector>.TrainingPair()
				{
					Class = BinaryClass.Positive,
					Item = new Vector(new double[] { 2.0, 1.0 })
				});

			pairs.Add(
				new BinaryClassifier<Vector>.TrainingPair()
				{
					Class = BinaryClass.Positive,
					Item = new Vector(new double[] { 1.0, 1.0 })
				});

			pairs.Add(
				new BinaryClassifier<Vector>.TrainingPair()
				{
					Class = BinaryClass.Positive,
					Item = new Vector(new double[] { 0.8, 0.8 })
				});

			return pairs;
		}

		private IList<BinaryClassifier<Vector>.TrainingPair> GetSimpleTrainingSetD2()
		{
			var pairs = new List<BinaryClassifier<Vector>.TrainingPair>(10);

			pairs.Add(
				new BinaryClassifier<Vector>.TrainingPair()
				{
					Class = BinaryClass.Negative,
					Item = new Vector(new double[] { -1.0, -1.0 })
				});

			pairs.Add(
				new BinaryClassifier<Vector>.TrainingPair()
				{
					Class = BinaryClass.Positive,
					Item = new Vector(new double[] { 1.0, 1.0 })
				});

			pairs.Add(
				new BinaryClassifier<Vector>.TrainingPair()
				{
					Class = BinaryClass.Negative,
					Item = new Vector(new double[] { -2.0, -2.0 })
				});

			return pairs;
		}

		private IList<BinaryClassifier<Vector>.TrainingPair> GetSimpleValidationSetD2()
		{
			var pairs = new List<BinaryClassifier<Vector>.TrainingPair>(10);

			pairs.Add(
				new BinaryClassifier<Vector>.TrainingPair()
				{
					Class = BinaryClass.Negative,
					Item = new Vector(new double[] { -2.0, -2.0 })
				});

			pairs.Add(
				new BinaryClassifier<Vector>.TrainingPair()
				{
					Class = BinaryClass.Positive,
					Item = new Vector(new double[] { +2.0, +2.0 })
				});

			return pairs;
		}

		private IList<BinaryClassifier<Vector>.TrainingPair> GetHorizontalTrainingSetD2()
		{
			var pairs = new List<BinaryClassifier<Vector>.TrainingPair>(10);

			pairs.Add(
				new BinaryClassifier<Vector>.TrainingPair()
				{
					Class = BinaryClass.Negative,
					Item = new Vector(new double[] { -1.0, -1.0 })
				});

			pairs.Add(
				new BinaryClassifier<Vector>.TrainingPair()
				{
					Class = BinaryClass.Negative,
					Item = new Vector(new double[] { -1.0, 1.0 })
				});

			pairs.Add(
				new BinaryClassifier<Vector>.TrainingPair()
				{
					Class = BinaryClass.Negative,
					Item = new Vector(new double[] { -2.0, 0.0 })
				});

			//pairs.Add(
			//  new BinaryClassifier<Vector>.TrainingPair()
			//  {
			//    Class = BinaryClass.Positive,
			//    Item = new Vector(new double[] { 1.0, 1.0 })
			//  });

			pairs.Add(
				new BinaryClassifier<Vector>.TrainingPair()
				{
					Class = BinaryClass.Positive,
					Item = new Vector(new double[] { 1.0, 0.0 })
				});

			pairs.Add(
				new BinaryClassifier<Vector>.TrainingPair()
				{
					Class = BinaryClass.Positive,
					Item = new Vector(new double[] { 2.0, 0.0 })
				});


			return pairs;
		}

		private IList<BinaryClassifier<Vector>.TrainingPair> GetHorizontalValidationSetD2()
		{
			var pairs = new List<BinaryClassifier<Vector>.TrainingPair>(10);

			pairs.Add(
				new BinaryClassifier<Vector>.TrainingPair()
				{
					Class = BinaryClass.Negative,
					Item = new Vector(new double[] { -0.5, -2.0 })
				});

			pairs.Add(
				new BinaryClassifier<Vector>.TrainingPair()
				{
					Class = BinaryClass.Positive,
					Item = new Vector(new double[] { 0.5, 1.0 })
				});

			return pairs;
		}

		#endregion

		#region Utility private methods

		private void VectorTrainAndTest(
			Kernel<Vector> kernel,
			IList<BinaryClassifier<Vector>.TrainingPair> trainingPairs,
			IList<BinaryClassifier<Vector>.TrainingPair> validationPairs,
			double slackPenalty)
		{
			if (kernel == null) throw new ArgumentNullException("kernel");

			var classifier =
				//new CgLineSearchBinaryClassifier<Vector>(kernel);
				//new CgNewtonBinaryClassifier<Vector>(kernel);
				new CgChunkingLineSearchBinaryClassifier<Vector>(kernel);

			classifier.ChunkingOptions.MaxChunkSize = 2;

			//classifier.SolverOptions.BarrierScaleFactor = 10.0;
			//classifier.SolverOptions.DualityGap = 1e-7;
			//classifier.SolverOptions.KrylovIterationsCountLogOffsetStart = 1.0;

			classifier.Train(trainingPairs, slackPenalty);

			int correctPredictions =
				validationPairs.Sum(p => Math.Sign(classifier.Discriminate(p.Item)) == (int)p.Class ? 1 : 0);

			Assert.AreEqual(
				validationPairs.Count,
				correctPredictions,
				"Should have found all predictions correct."
				);
		}

		#endregion
	}
}