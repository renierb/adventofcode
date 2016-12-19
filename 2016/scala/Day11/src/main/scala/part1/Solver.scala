package part1

import scala.collection.parallel.immutable.ParSet

trait Solver extends DomainDef {

  val startState: Elevator

  case class StateSearch(active: ParSet[Elevator], explored: ParSet[Elevator] = ParSet.empty) {
    def next: StateSearch =
      StateSearch(active.flatMap(_.legalFloors) diff explored, explored = active)

    def found: Boolean = active.exists(isGoal)
  }

  object StateSearch {
    def initial(elevator: Elevator) = StateSearch(active = ParSet(elevator))
  }

  def solve: Int = {
    Stream
      .iterate(StateSearch.initial(startState))(_.next)
      .indexWhere(_.found)
  }
}