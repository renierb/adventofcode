package part1

trait Solver extends DomainDef {

  def done(b: Block): Boolean = b.b1 == goal

  def neighborsWithHistory(b: Block, history: List[Move]): Stream[(Block, List[Move])] = {
    for( (block, move) <- b.legalNeighbors.toStream ) yield (block, move :: history)
  }

  def newNeighborsOnly(neighbors: Stream[(Block, List[Move])],
                       explored: Set[Block]): Stream[(Block, List[Move])] = {
    for( (block, moveList) <- neighbors if !explored.contains(block) ) yield (block, moveList)
  }

  def from(initial: Stream[(Block, List[Move])],
           explored: Set[Block]): Stream[(Block, List[Move])] = initial match {
    case Stream.Empty => Stream.empty

    case (block, moveList) #:: tail =>
      val newExplored = explored + block
      val newNeighbors = newNeighborsOnly(neighborsWithHistory(block, moveList), newExplored)

      newNeighbors ++ from(tail ++ newNeighbors, newExplored)
  }

  lazy val pathsFromStart: Stream[(Block, List[Move])] =
    from((startSanta, List[Move]()) #:: Stream[(Block, List[Move])](), Set())

  lazy val pathsToGoal: Stream[(Block, List[Move])] = {
    pathsFromStart filter {
      case (b, _) => b.b1 == goal
    }
  }

  lazy val solution: List[Move] = pathsToGoal match {
    case Stream.Empty => Nil
    case (block, moveList) #:: tail => moveList
  }
}