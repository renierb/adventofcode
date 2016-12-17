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
                   movement: Stream[(Elevator, List[Move])],
                   explored: Set[Elevator]): Stream[(Elevator, List[Move])] = {
    initial match {
      case Stream.Empty => movement

      case (elevator, moveList) #:: tail =>
        val moreExplored = explored + elevator
        val moreMovement = newFloorsOnly(floorsWithHistory(elevator, moveList), moreExplored)
        from(tail ++ moreMovement, moreMovement #::: movement, moreExplored)
    }
  }

  lazy val tripsFromStart: Stream[(Elevator, List[Move])] =
    from(Stream((startState, List[Move]())), Stream(), Set())

  lazy val tripsToGoal: Stream[(Elevator, List[Move])] =
    tripsFromStart.filter {
      case (e, _) => isGoal(e)
    }

  private def isGoal(e: Elevator) = {
    e.floor == 3 && (0 to 2).forall(e.items(_).isEmpty)
  }

  lazy val solution: List[Move] = tripsToGoal match {
    case Stream.Empty => Nil
    case (elevator, moveList) #:: tail => moveList
  }
}