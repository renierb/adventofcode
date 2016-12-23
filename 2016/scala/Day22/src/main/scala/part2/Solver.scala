package part2

import part1.InputParser.Node

import scala.annotation.tailrec
import scala.collection.parallel.immutable.ParSet

class Solver(inputNodes: List[Node]) extends DomainDef {

  override val maxX: Int = inputNodes.maxBy(_.x).x
  override val maxY: Int = inputNodes.maxBy(_.y).y

  private val goal = inputNodes.filter(n => n.y == 0).maxBy(_.x)
  private val emptyOne = inputNodes.filter(n => n.used == 0).head

  private var grid = inputNodes.map {
    case Node(x, y, size, used) =>
      val pos = Pos(x, y)
      if (used <= emptyOne.size)
        Pos(x, y) -> '.'
      else
        Pos(x, y) -> '#'
  }.toMap

  grid = grid.updated(Pos(goal.x, goal.y), 'G')
  grid = grid.updated(Pos(emptyOne.x, emptyOne.y), '_')

  private val startState: StorageGrid = StorageGrid(Pos(emptyOne.x, emptyOne.y), grid)

  def solve: Int = {
    iterate(ParSet(startState))
  }

  @tailrec
  private def iterate(active: ParSet[StorageGrid], explored: ParSet[StorageGrid] = ParSet.empty, iteration: Int = 0): Int = {
    if (active.isEmpty)
      -1
    else if (active.exists(_.isDone)) {
      iteration
    } else {
      iterate(active.flatMap(_.newStates) diff explored, active, iteration + 1)
    }
  }
}
