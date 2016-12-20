import org.scalatest._
import part2.Solver

class Part2UnitTests extends FunSuite {

//  test("part 2: 2 elves") {
//    val solver = new Solver(2)
//    val actual = solver.solve
//    assertResult(1)(actual)
//  }
//
//  test("part 2: 3 elves") {
//    val solver = new Solver(3)
//    val actual = solver.solve
//    assertResult(3)(actual)
//  }
//
//  test("part 2: 5 elves") {
//    val solver = new Solver(5)
//    val actual = solver.solve
//    assertResult(2)(actual)
//  }

  test("part 2: 6 elves") {
    val solver = new Solver(6)
    val actual = solver.solve
    assertResult(3)(actual)
  }
}
