package part1

import scala.annotation.tailrec
import scala.collection.parallel.immutable.ParSet

class Solver(inputMap: Vector[String], val maxLocations: Int) extends DomainDef {

  override val maxX: Int = inputMap.maxBy(_.length).length - 1
  override val maxY: Int = inputMap.length - 1

  val startPos: (Int, Int) = inputMap.zipWithIndex.filter {
    case (line, _) => line.indexOf('0') >= 0
  }.map {
    case (line, y) => (line.indexWhere(_ == '0'), y)
  }.head

  private val startState: Map = Map(Pos(startPos._1, startPos._2), 0, inputMap)

  def solve: Int = {
    iterate(ParSet(startState))
  }

  @tailrec
  private def iterate(active: ParSet[Map], explored: ParSet[Map] = ParSet.empty, iteration: Int = 0): Int = {
    if (active.isEmpty)
      -1
    else if (active.exists(_.isDone)) {
      iteration
    } else {
      iterate(active.flatMap(_.newStates) diff explored, active, iteration + 1)
    }
  }
}
