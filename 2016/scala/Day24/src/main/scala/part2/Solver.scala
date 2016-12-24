package part2

class Solver(inputMap: Vector[String], maxLocations: Int) extends part1.Solver(inputMap, maxLocations) {

  override val goal = Some(Pos(startPos._1, startPos._2))
}
