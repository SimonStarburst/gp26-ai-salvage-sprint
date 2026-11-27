namespace SalvageSprint.Core
{
    // Week 1 assignment starter. Work through README stages in this same project.
    // STAGE 2: distinguish visible candidates from unknown hidden objects.
    // STAGE 3: retain a usable remembered target. STAGE 4: use failed-route feedback.
    // Delivery is supplied so the unmodified game has a complete scoring loop.
    public static class DecisionPolicy
    {
        public const string Label = "Part 1 starter: nearest candidate";

        // Infer the agent's facts from its observation. This is a simple, naive implementation that ignores visibility and memory.
        public static AgentFacts Infer(Observation observation)
        {
            if (observation.Carrying)
                return new AgentFacts(true, -1, false);

            foreach (Candidate candidate in observation.Candidates)
            {
                // If the ID of the candidate in the loop is the same as the ID being observed:
                if (candidate.Id == observation.CurrentTargetId && IsEligible(candidate))
                {
                    // Returning that carrying = false, candidate.Id is selected target Id and
                    // retainedTarget as true.
                    return new AgentFacts(false, candidate.Id, true);
                }
            }

            int nearest = -1;
            float distance = float.PositiveInfinity;

            // Serches for nearest eligible target if previous loop didn't return
            // target
            foreach (Candidate candidate in observation.Candidates)
            {
                if (IsEligible(candidate) && candidate.DistanceSquared < distance) 
                {
                    nearest = candidate.Id;
                    distance = candidate.DistanceSquared;
                }
            }
            return new AgentFacts(false, nearest, false);
        }

        public static Decision Choose(Observation observation) => Choose(Infer(observation));

        public static Decision Choose(AgentFacts facts)
        {
            // Deliberately naive: ignores RetainedTarget and any other facts.
            if (facts.NeedsDelivery)
            {
                return new Decision(TaskKind.Deliver, -1, "Cargo held: return to depot");
            }

            // If there's no usable target, sets Rival to explore with it's Depot as an end goal
            if (!facts.HasUsableTarget)
            {
                return new Decision(TaskKind.Explore, -1, "No usable target, explore to next exploration point");
            }
            
            return facts.HasUsableTarget

                ? new Decision(TaskKind.Collect, facts.SelectedTargetId, "Nearest candidate; reselected each tick")
                : new Decision(TaskKind.Explore, -1, "No candidate: visit the next survey point");
        }

        // Bool to see if a candidate is eligible to be sought after:
        // it needs to be available, visible OR remembered and it cannot be blocked
        // This is just to save space and not having to write out longer code than needed.
        private static bool IsEligible(Candidate candidate)
        {
            return candidate.Available
                && (candidate.Visible || candidate.Remembered)
                && !candidate.Blocked;
        }
    }
}
