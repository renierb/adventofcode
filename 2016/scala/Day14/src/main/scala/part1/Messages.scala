package part1

import scala.concurrent.duration.Duration

case object Calculate

case class PasswordItem(index: Int)
case class Answer(index: Int, duration: Duration, stop: Boolean = false)

case class FindTriplet(from: Int, until: Int)
case class TripletAnswer(value: Option[String], index: Int, until: Int)

case class FindFullHouse(value: String, from: Int, until: Int)
case class FullHouseAnswer(value: Option[String], from: Int, index: Int)