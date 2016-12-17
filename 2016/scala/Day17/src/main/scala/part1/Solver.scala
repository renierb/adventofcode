package part1

trait Solver extends DomainDef {

  def done(b: Room): Boolean = b.pos == goal

  def neighborsWithHistory(b: Room, history: List[Move]): Stream[(Room, List[Move])] = {
    for ((block, move) <- b.legalNeighbors.toStream) yield (block, move :: history)
  }

  def newNeighborsOnly(neighbors: Stream[(Room, List[Move])],
                       explored: Set[Room]): Stream[(Room, List[Move])] = {
    for ((block, moveList) <- neighbors if !explored.contains(block)) yield (block, moveList)
  }

  def from(initial: Stream[(Room, List[Move])],
           explored: Set[Room]): Stream[(Room, List[Move])] =
    if (initial.isEmpty) {
      Stream.empty
    } else {
      val newExplored = explored ++ initial.map(_._1)

      val newMovement = for {
        (room, moveList) <- initial
        rooms <- newNeighborsOnly(neighborsWithHistory(room, moveList), newExplored)
      } yield rooms

      newMovement #::: from(newMovement, newExplored)
    }

  lazy val pathsFromStart: Stream[(Room, List[Move])] =
    from((startRoom, List[Move]()) #:: Stream[(Room, List[Move])](), Set())

  lazy val pathsToGoal: Stream[(Room, List[Move])] = {
    pathsFromStart filter {
      case (b, _) => b.pos == goal
    }
  }

  lazy val shortestPath: List[Move] = pathsToGoal match {
    case Stream.Empty => Nil
    case (_, moveList) #:: _ => moveList
  }

  lazy val longestPath: List[Move] =
    if (pathsToGoal.isEmpty)
      Nil
    else
      pathsToGoal.last._2
}