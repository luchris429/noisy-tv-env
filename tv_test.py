import numpy as np

from unityagents import UnityEnvironment

env_name = "tv_maze"
env = UnityEnvironment(file_name=env_name, worker_id = 2)
print(str(env))

default_brain = env.brain_names[0]
brain = env.brains[default_brain]
train_mode = False
prevState = None
np.random.seed = 13

d = {"startLoc" : 1, "render" : 1.0, "tv" : 0.0, "door" : 1.0}
for episode in range(300):
    env_info = env.reset(train_mode=train_mode, config = d)[default_brain]
    done = False
    episode_rewards = 0
    for i in range(1000):
        print(env_info.states)
        if brain.action_space_type == 'continuous':

            act = np.random.randn(len(env_info.agents), brain.action_space_size)
            if False:
                quaternion = [1,0,0,0]
                quaternion = np.array(quaternion)
                act[:, :4] = quaternion
            env_info = env.step(act)[default_brain]
        else:
            a = int(raw_input("input: "))
            env_info = env.step(a)[default_brain]

        done = np.any(env_info.local_done)
        print(i)
        print(env_info.rewards)
        print(done)


env.close()