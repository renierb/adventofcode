package part1

import scala.annotation.tailrec

trait Solver extends DomainDef {

  val startState: Elevator

  def floorsWithHistory(e: Elevator, history: List[Move]): Stream[(Elevator, List[Move])] = {
    for ((elevator, move) <- e.legalFloors.toStream) yield (elevator, move :: history)
  }

  def newFloorsOnly(adjacent: Stream[(Elevator, List[Move])],
                    explored: Set[Elevator]): Stream[(Elevator, List[Move])] = {
    for ((elevator, moveList) <- adjacent if !explored.contains(elevator)) yield (elevator, moveList)
  }

  @tailrec
  private def from(initial: Stream[(Elevator, List[Move])],
                   results: Stream[(Elevator, List[Move])],
                   explored: Set[Elevator]): Stream[(Elevator, List[Move])] = {
    if (initial.isEmpty) {
      results
    } else {
      initial match {
        case Stream.Empty => Stream.empty

        case (elevator, moveList) #:: tail =>
          val newExplored = explored + elevator
          val nextFloors = newFloorsOnly(floorsWithHistory(elevator, moveList), newExplored)

          from(tail ++ nextFloors, results #::: nextFloors, newExplored)
      }
    }
  }

  lazy val tripsFromStart: Stream[(Elevator, List[Move])] =
    from(Stream((startState, List[Move]())), Stream(), Set())

  lazy val tripsToGoal: Stream[(Elevator, List[Move])] = {
    tripsFromStart.filter(e => isGoal(e._1))
  }

  private def isGoal(e: Elevator) = {
    e.floor == 3 && (0 to 2).forall(e.items(_).isEmpty)
  }

  lazy val solution: List[Move] = tripsToGoal match {
    case Stream.Empty => Nil
    case (elevator, moveList) #:: tail => moveList
  }
}