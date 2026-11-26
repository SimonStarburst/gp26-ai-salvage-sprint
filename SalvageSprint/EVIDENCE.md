# Week 1 evidence

Keep this short. Bullet points, a small table and one diagram or code trace are enough. This is your checkpoint record, not an extra report. Replace the prompts as you work.



**Name:** Simon
**Exact Unity version:** 6000.3.26f1
**Checkpoint commit / repository:** Initial Commit
**Current stage and anything unfinished:** Stage 0



## Initial prediction



**Three observations the rival needs, including one it might not have:**

Visual observation of a scrap

Proximity of scraps

One thing the rival needs, that it currently doesn't, is some type of memory for which scraps it has seen.



**Seeing a pickup across a gap proves / does not prove reachability because:**

Visual reachability does not automatically mean you can get to it.



**One question about NPC behaviour:**

Question regarding how companions in Skyrim/Fallout 4 works



## One complete trace

Use one actual situation. Include the observation data, derived facts, selected task and reason, movement request/result, and how feedback affects a later decision. Identify the responsible code locations. A tiny state diagram can accompany this trace.



## Scenario record

|Scenario|Predicted result.|Observed result and short evidence / remaining issue|
|-|-|-|
|Normal loop and restart|Predicting no issues with regular gameplay.||
|Carrying cargo|It will carry cargo back to depot and then continue it's adventure.||
|Hidden / remembered pickup|It will go for remembered Scrap Before the hidden one, hopefully||
|Target taken|It will go for either closeset it can see or closest from memory.||
|Unreachable target|It should go after a previously found target, so a remembered target, target it sees or patrol after one.|Instead of going after other targets, it focuses in on the one it is unable to reach and instead of going anywhere, the Rival just stands there|
|No usable pickup|Patrolling until it realises there's no usable pickups.||



Supplied test result, with any added tests identified:



Live walkthrough checked by / recording link if used:



## One correction and one independent check

**The original weakness and why it happened:**



The original weakness I chose to investigate was how the Rival focuses in on the unreachable target, even though it shouldn't be able to see it. It happened because the script itself didn't care whether a candidate was blocked or not.



**What I changed and why it addresses the cause:**



In the foreach-loop (Candidate candidate in observation.Candidates), it only looked if the candidate was available and within distance, it didn't check for anything else. By changing it from "candidate.Available" to "candidate.Available \&\& (candidate.Visible || candidate.Remembered) \&\& !candidate.Blocked" we could instead look if the candidate was available, if it was visible/remembered and if it wasn't blocked.



**My counterexample or experiment: prediction, observed result, and one limitation:**



My experiment was a bunch of else if-statements, one of the earliest ones being: else if (candidate.Available \&\& candidate.DistanceSquared < distance \&\& !candidate.Blocked \&\& candidate.Visible).

I was struggling a bit to find the logic, which is why I found inspiration.





## Collaboration and help

**People I discussed or playtested with:**




**Code, sources or substantive tools/AI assistance I used, and how I verified them:**



I did look at the finished written code by the teacher, because I was on the right track with how to write the required code but I was hitting some issues. By taking some inspiration from that script, I managed to see what I did wrong and could add the code I wanted.


**If unfinished: what works, exact reproduction steps, and the help I need:**





