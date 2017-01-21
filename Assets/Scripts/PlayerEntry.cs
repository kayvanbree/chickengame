[System.Serializable]
public class PlayerEntry
{
    bool active = false;
    public JoinPanel JoinPanel;
    public LeavePanel LeavePanel;

    public void Activate()
    {
        this.active = true;
        JoinPanel.gameObject.SetActive(false);
        LeavePanel.gameObject.SetActive(true);
    }

    public void Deactivate()
    {
        this.active = false;
        JoinPanel.gameObject.SetActive(true);
        LeavePanel.gameObject.SetActive(false);
    }

    public bool IsActive()
    {
        return active;
    }
}
