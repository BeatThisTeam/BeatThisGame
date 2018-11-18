using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TilesAttack : MonoBehaviour {

    public GroundColorChanger groundControl;
    public GroundSections ground;
    public CharacterController player;
    public float percentage;
    private List<Section> sections = new List<Section>();

    private struct Section {

        public int ring;
        public int sector;
        public bool hurts;
    }

	public void StartAttack(float noteToPlayInSeconds) {
            
        if (noteToPlayInSeconds - SongManager.Instance.SongPositionInSeconds <= 0) {
            if (sections.Count == 0) {

                int playerRingPosCorrected = player.ringIndex + ground.rings.Count;
                int playerSectPosCorrected = player.faceIndex + ground.rings[0].faces.Count;

                for (int i = playerRingPosCorrected - 1; i <= playerRingPosCorrected + 1; i++) {
                    for (int j = playerSectPosCorrected - 2; j <= playerSectPosCorrected + 2; j++) {
                        Section section = new Section();
                        section.ring = (i%3);
                        section.sector = (j%9);
                        section.hurts = false;
                        if ((section.ring != player.ringIndex || section.sector != player.faceIndex) && Random.Range(0f, 1f) >= percentage) {
                            groundControl.ChangeColor(section.ring, section.sector);
                            section.hurts = true;
                        }
                        sections.Add(section);
                    }
                }
                    
            } else {
                for (int i = 0; i < sections.Count; i++) {
                    groundControl.ChangeColor(sections[i].ring, sections[i].sector);
                    Section section = new Section();
                    section = sections[i];
                    section.hurts = !section.hurts;
                    sections[i] = section;
                }
            }
        }
    }

    public void ClearSections() {

        for (int i = 0; i < sections.Count; i++) {
            if (sections[i].hurts) {
                groundControl.ChangeColor(sections[i].ring, sections[i].sector);
                Section section = new Section();
                section = sections[i];
                section.hurts = !section.hurts;
                sections[i] = section;
            }
        }
        sections.Clear();
    }
}
