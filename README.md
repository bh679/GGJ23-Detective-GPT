# GGJ: Detective GPT

A VR murder mystery party game where the AI decides what happened and who is guilty!<br />

The game starts when one of the players murder another.<br />
In comes, Detective Gerald-Patrick-Thompson!<br />

Provide the ai evidence to point it towards who to convict.<br />
Who will be convicted for the dastardly crime? <br />
Can you get it to the right conclusion?<br />
Can you get away with murder?<br />
Can you get away with frame someone else?<br />
How will the story unfold? <br />


The Detective GPT uses its AI detective work to create a story based on the information it has collected, to deduce who it thinks the murder is, and how it all unfolded.<br />
The all-knowing ai is always right, even when its wrong.<br />
<i>"Yours are human brains, and thus prone to human error! I am only capable of hilarious robot error!"</i><br />

# Download Now

 - [Google Drive: Oculus Quest](https://drive.google.com/drive/folders/17-44bLbAO07-fiLAMy23haLw4sVXC7Qr?usp=share_link)
 - [Google Drive: Windows Flatscreen](https://drive.google.com/drive/folders/1LtNZRFeVXAgcSXCVRS1q5PRxwngAcVJZ?usp=share_link)

[Join the discord](https://discord.gg/bd8KrVhd8u) to coordinate a game with the developers and community  <br />

## Links
Website: https://tim-randall.github.io/detectiveGPTweb/ <br />
GGJ Page: https://globalgamejam.org/2023/games/detective-gpt-3<br />
SideQuest: (coming soon)<br />
Discord: https://discord.gg/bd8KrVhd8u  <br />


#### Team Pet Vacuum 
- [Brennan Hatton](https://github.com/bh679) - Project Lead, Lead Programmer & Implmentation  <br />
- Wilson Taylor  - Narrative Lead & DectiveGPT Voice Actor<br />
- [Tim Christie](https://github.com/timchristie)  - Comic and Game Designer<br />
- [Clemant Chan](https://github.com/ClemAuschan)  - All Things Sound<br />
- [Tim Randall](https://github.com/Tim-Randall)  - Website Developer<br />
- [Dale Keaveny](https://github.com/LordBeardsteak) - Music Composor<br />

# Open Source Project Guide

## Requirements:
[Unity 2021.3.16/f1](https://unity3d.com/unity/whats-new/2021.3.15) (free)   <br />
[BNG VRIF 1.82](https://assetstore.unity.com/packages/templates/systems/vr-interaction-framework-161066) (paid - import yourself)   <br />
[Synty Polygon Horror Mansion](https://assetstore.unity.com/packages/3d/environments/fantasy/polygon-horror-mansion-low-poly-3d-art-by-synty-213346) (paid) <br />
[RPG Character Mecanim Animation Pack](https://assetstore.unity.com/packages/3d/animations/rpg-character-mecanim-animation-pack-63772) (paid) <br />
[CleanFlatIcons](https://assetstore.unity.com/packages/2d/gui/icons/clean-flat-icons-98117)

## Setup Guide <br />
 - Clone
 - Rename ``BNG Framework1`` to ``BNG Framework``
 - Open in Unity (Enter safe mode)
 - Import [BNG VRIF](https://assetstore.unity.com/packages/templates/systems/vr-interaction-framework-161066) (without project settings, yes to updating scripts - [Docs](https://wiki.beardedninjagames.com/en/Overview/InstallationGuide))
 - Get private keys ([open ai](https://beta.openai.com/account/api-keys), [ms cog](https://azure.microsoft.com/en-us/products/cognitive-services/text-to-speech/), [PUN](https://www.photonengine.com/pun))
 - Import [Synty Polygon Horror Mansion](https://assetstore.unity.com/packages/3d/environments/fantasy/polygon-horror-mansion-low-poly-3d-art-by-synty-213346)
 - Import [RPG Character Mecanim Animation Pack](https://assetstore.unity.com/packages/3d/animations/rpg-character-mecanim-animation-pack-63772), but without:
    - the ``Editor folder``
    - ``Assets\ExplosiveLLC\RPG Character Mecanim Animation Pack\Demo Elements\Code\GUIControls.cs`` <br />
 - Import [CleanFlatIcons](https://assetstore.unity.com/packages/2d/gui/icons/clean-flat-icons-98117)

After cloning the project and opening in Unity, you may be asked to 'enter safe mode'. Do this.
You will need to import VRIF.
Make sure you have purchased VRIF from the link above

Before importing VRIF - consider importing it to a new project and following the guide for updating a new project on  to avoid replacing system settings, or unselect "Project Settings" in the Import Unity pacakge manager.

To import VRIF, do so from the assets store page or
Windows -> Package Manager -> Packages: My Assets -> Search: `VRIF` -> Download/Import (make sure you are using version 1.82)
*If importing dirtectly, unselect Project settings here. Do not "Import All"

Script Update Consent
 - Yes, for these and other files that might be found later. 

### Get Private Keys
If youre part of the team, get the unity pack for the below from Brennan.
If youre setting up your own copy, you'll need:
 - Photon App ID ([guide](https://doc.photonengine.com/realtime/current/getting-started/obtain-your-app-id#:~:text=The%20App%20ID%20is%20a,ID%20just%20click%20on%20it.))
 - Discord WebHooks ([guide](https://discordjs.guide/popular-topics/webhooks.html#what-is-a-webhook))
 - [OpenAI GPT API](https://platform.openai.com/account/api-keys)
 - [Microsoft Cognative Services](https://azure.microsoft.com/en-us/products/cognitive-services/text-to-speech/) (for text to speech)
 

## Pre-Installed <br />
[PUN 2](https://assetstore.unity.com/packages/tools/network/pun-2-free-119922)<br />
[Photon Voice 2 - 2.50](https://assetstore.unity.com/packages/tools/audio/photon-voice-2-130518)<br />
[BrennanHattons Unity Tools](https://github.com/bh679/Unity-Tools) <br />
[BrennanHatton PUN Discord Webhook](https://github.com/bh679/Unity-Discord-Webhook-Tools) <br />
[BrennanHatton VRIF PUN Assets](https://github.com/bh679/VRIF-PUN-Assets) <br />
[Parallel Sync 1.5.1](https://github.com/VeriorPies/ParrelSync)  <br />
This is was made from [VR MMO AI Tempalte](https://github.com/bh679/VR-MMO-AI-Template)

## License
[Attribution-ShareAlike 4.0 International (CC BY-SA 4.0)](https://creativecommons.org/licenses/by-sa/4.0/)
