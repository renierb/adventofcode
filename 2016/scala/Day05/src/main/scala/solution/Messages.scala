package solution

import scala.concurrent.duration.Duration

case object Calculate
case class Work(from: Int, until: Int)

case class PasswordItem(index: Int, code: Char, codeIndex: Int = -1)
case class Result(value: List[PasswordItem])

case class Answer(answer: String, duration: Duration, stop: Boolean = false)