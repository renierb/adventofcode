package part1

trait Solver extends DomainDef {

  val startState: Elevator

  def floorsWithHistory(e: Elevator, history: List[Move]): Stream[(Elevator, List[Move])] = {
    for ((elevator, move) <- e.legalFloors.toStream) yield (elevator, move :: history)
  }

  def newFloorsOnly(adjacent: Stream[(Elevator, List[Move])],
                    explored: Set[Elevator]): Stream[(Elevator, List[Move])] = {
    for ((elevator, moveList) <- adjacent if !explored.contains(elevator)) yield (elevator, moveList)
  }

  def from(initial: Stream[(Elevator, List[Move])],
           explored: Set[Elevator]): Stream[(Elevator, List[Move])] = initial match {
    case Stream.Empty => Stream.empty

    case (elevator, moveList) #:: tail =>
      val newExplored = explored + elevator
      val newNeighbors = newFloorsOnly(floorsWithHistory(elevator, moveList), newExplored)

      newNeighbors ++ from(tail ++ newNeighbors, newExplored)
  }

  lazy val tripsFromStart: Stream[(Elevator, List[Move])] =
    from((startState, List[Move]()) #:: Stream[(Elevator, List[Move])](), Set())

  lazy val tripsToGoal: Stream[(Elevator, List[Move])] = {
    tripsFromStart.filter {
      case (e, _) => e.floor == 3 && (0 to 2).forall(e.items(_).isEmpty)
    }
  }

  lazy val solution: List[Move] = tripsToGoal match {
    case Stream.Empty => Nil
    case (elevator, moveList) #:: tail => moveList
  }
}